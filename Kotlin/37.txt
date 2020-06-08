package com.nurflugel.hocon.parsers

import com.nurflugel.hocon.parsers.domain.*
import org.apache.commons.lang3.StringUtils


class HoconParser {
    companion object {

//    /**
//     * this assumes the lines being parsed are pure property lines - just
//     * stuff like aaa.bbb.ccc.dd=true, no maps
//     */
//    fun convertPropertiesToConf(existingLines: List<String>): MutableList<String> {
//      val propsMap: PropertiesMap = populatePropsMap(existingLines)
//      return generateConfOutput(propsMap)
//    }
//
        /** Go through the map and see if there are any more than one key for any sub-map */
        fun isSingleKeyValue(map: HoconMap): Boolean {
            if (map.getKeys().size > 1) return false

            val values = map.getValues()
            if (values.isEmpty()) return false

            val value = values.first()// we only care about the first branch (we checked above for more)
            return when (value) {
                is HoconString -> true
                is HoconMap -> isSingleKeyValue(value)
                else -> false
            }
        }


        /** We're taking in a list of key-value pairs (no {} mapping and creating the map */
        public fun populatePropsMap(existingLines: List<String>): PropertiesMap {
            val propsMap = PropertiesMap()

            // have to iterate the old-fashioned way, as we may have to go forwards to grab lists, comments, etc.
            val index = IndexIndent()
            while (index.index < existingLines.size) {
                processLine(existingLines[index.index].trim(), existingLines, propsMap, index)
            }

            return propsMap
        }

        private fun processLine(
            line: String,
            existingLines: List<String>,
            propsMap: PropertiesMap,
            index: IndexIndent
        ): HoconType {
            return when {
                // empty line, skip for now
                isEmptyLine(line) -> processBlankLine(index)

                // comments
                isComment(line) -> processComment(existingLines, index, line, propsMap)

                // it's the beginning of a list
                isListStart(line) -> processList(existingLines, index, line, propsMap)

                // beginning of a map
                isMapStart(line) -> addLevelToKeyStack(line, index)

                // or the end of one
                isMapEnd(line) -> popStack(line, index)

                // properties
                isProperty(line) -> processProperty(line, propsMap, index)

                // includes
                isInclude(line) -> processInclude(line, propsMap, index)

                // wtf?
                else -> processUnknown(index, line)
            }
        }

        private fun popStack(line: String, index: IndexIndent): HoconType {
            val numMatches = StringUtils.countMatches(line, "}")
            repeat((0 until numMatches).count()) { index.keyStack.pop() }
            index.increment()
            return HoconVoid()
        }

        private fun isEmptyLine(line: String) = StringUtils.isEmpty(line)

        private fun processBlankLine(index: IndexIndent): HoconBlankLine {
            index.increment()
            return HoconBlankLine()
        }


        private fun isComment(line: String) = line.startsWith("//") || line.startsWith("#")
        private fun isInclude(line: String) = line.startsWith("include")
        private fun isListStart(line: String) = line.contains("[")
        private fun isMapEnd(line: String) = !line.contains("=") && line.contains("}")
        private fun isMapStart(line: String) = !line.contains("=") && line.contains("{")
        private fun isProperty(line: String) = line.contains("=")

        private fun processInclude(line: String, propsMap: PropertiesMap, index: IndexIndent): HoconType {
            propsMap.addInclude(line)
            index.increment()
            return HoconInclude(line)
        }

        private fun processUnknown(index: IndexIndent, line: String): HoconType {
            println("Unknown case in parsing line $index: $line")
            index.increment()
            return HoconUnknown()
        }

        /** take the line and convert it into a key/value pair */
        private fun processProperty(line: String, propsMap: PropertiesMap, index: IndexIndent): HoconType {
            val prefix = index.keyStack
                .joinToString(separator = ".")
            val key = StringUtils.substringBefore(line, "=").trim()
            val value = HoconString(StringUtils.substringAfter(line, "=").trim())

            // the full key includes everything in the stack as a prefix
            val pair = when {
                prefix.isNotBlank() -> HoconPair("$prefix.$key", value)
                else -> HoconPair(key, value)
            }

            addToPropsMap(pair, propsMap)// todo deal with key path?
            index.increment()
            return pair
        }

        /** take the line and start processing the list */
        private fun processList(
            existingLines: List<String>,
            index: IndexIndent,
            line: String,
            propsMap: PropertiesMap
        ): HoconList {
            // two cases - it's a list all in one line, or it's "vertical"
            return when {
                line.contains("]") -> {
                    processSingleLineList(line, propsMap, index)
                }
                else -> {
                    processMultilineList(line, index, existingLines, propsMap)
                }
            }
        }

        private fun processMultilineList(
            line: String,
            index: IndexIndent,
            existingLines: List<String>,
            propsMap: PropertiesMap
        ): HoconList {
            // now iterate until we find the closing bracket
            val listLines = mutableListOf<String>()
            val key = StringUtils.substringBefore(line, "=").trim()
            index.increment()
            // check for a value(s) on the same line as the opening bracket

            val possibleValue = StringUtils.substringAfter(line, "[").trim()
            addBracketValues(possibleValue, listLines)
            // process the rest
            while (index.index < existingLines.size) {
                val nextLine = existingLines[index.index].trim()
                if (nextLine.contains("]")) {
                    // check for any values on the same line as the ]
                    val possibleValue1 = StringUtils.substringBefore(nextLine, "]").trim()
                    addBracketValues(possibleValue1, listLines)
                    break
                }
                val valueToAdd = when {
                    nextLine.contains(",") -> nextLine.substringBefore(",")
                    else -> nextLine
                }
                listLines.add(valueToAdd)
                index.increment()
            }
            val list = HoconList(key, listLines)
            propsMap.addList(key, list)
            return list
        }

        /** take some text we found before or after brackets, parse any values for it */
        private fun addBracketValues(
            possibleValue: String,
            listLines: MutableList<String>
        ) {
            if (possibleValue.isNotBlank()) {
                val split = possibleValue.split(",")
                split.forEach { s ->
                    if (s.isNotBlank()) {
                        listLines.add(s.trim())
                    }
                }
            }
        }

        private fun processSingleLineList(
            line: String,
            propsMap: PropertiesMap,
            index: IndexIndent
        ): HoconList {
            val contents = StringUtils.substringBefore(StringUtils.substringAfter(line, "[").trim(), "]").trim()
            val key = StringUtils.substringBefore(line, "=").trim()
            val values = contents.split(",")
            val list = HoconList(key, values)// no comments for now
            propsMap.addList(key, list)
            index.increment()
            return list
        }

        /** read down the list of lines until we find a non-comment line.  Parse that, then, we bind the comment to that line's property key. */
        private fun processComment(
            existingLines: List<String>,
            index: IndexIndent,
            line: String,
            propsMap: PropertiesMap
        ): HoconType {
            val comments = mutableListOf<String>()
            comments.add(line)
            var nextLine: String
            // process the rest
            index.increment()// start at the next line

            while (index.index < existingLines.size) {
                nextLine = existingLines[index.index].trim()
                when {
                    isComment(nextLine) -> comments.add(nextLine)
                    else -> {
                        // we have a line that's not a comment - parse that and get the type
                        val type = processLine(nextLine, existingLines, propsMap, index)
                        when (type) {
                            is HoconPair -> type.value.comments = comments
                            else -> type.comments = comments
                        }
                        return type
                    }
                }
                index.increment()
            }
            return HoconBlankLine(listOf())
        }

        /** the line is a beginning of a map - recurse if needed */
        private fun addLevelToKeyStack(
            line: String,
            index: IndexIndent
        ): HoconType {
            val possibleValue = StringUtils.substringAfter(line, "{").trim()

//      if (possibleValue.isBlank()) {

                val key = StringUtils.substringBefore(line, "{").trim()

                // is the line a simple key? (a)
                return when {
                    key.contains(".") -> addNestedKeysToStack(line, index)
                    else -> addSingleKeyToStack(index, key)
                }
//      } 
//      else {
//        if (!possibleValue.startsWith("//") && !possibleValue.startsWith("#")) {
//           deal with values after the {
//          TODO("not implemented")
//        }else{
//          
//        }
//      }
        }

        private fun addNestedKeysToStack(line: String, index: IndexIndent): HoconType {
            // or a mapped key? (a.b.c.d)
            line.substringBefore("{").trim().split(".")
                .forEach {
                    index.keyStack.push(it)
                }
            index.increment()
            return HoconVoid()
        }

        // single key, now parse the values
        private fun addSingleKeyToStack(index: IndexIndent, key: String): HoconVoid {
            index.keyStack.push(key)
            index.increment()
            return HoconVoid()// tod return HoconKey?
        }


        // take the pair and add it to the properties map
        private fun addToPropsMap(keyValue: HoconPair, propsMap: PropertiesMap) {
            // the key path may be services.cpd.connection.retry, and the value might be 'true'
            val keyPath: List<String> = keyValue.key.split(".")

            var subMap: HoconMap = propsMap.map
            // if the map doesn't contain all the folders, create them
            (0 until keyPath.size - 1)
                .asSequence()
                .map { keyPath[it] }
                .forEach { subMap = getSubMap(subMap, it) }
            // now subMap is the lowest folder, just need to add the key
            subMap.set(keyPath.last(), keyValue.value)
        }

        // todo make a test
        private fun getSubMap(propsMap: HoconMap, folderName: String): HoconMap {
            if (!propsMap.containsKey(folderName)) {
                val newMap = HoconMap(folderName)
                propsMap.set(folderName, newMap)
                return newMap
            }
            val hoconType = propsMap.get(folderName)
            if (hoconType is HoconString) {
                println("""Expected a folder, but found a key for "$folderName"""")
            }
            return hoconType as HoconMap
        }
    }
}