package me.samboycoding.xamarindecomp

fun <T> Array<T>.getSafe(index: Int): T? {
    if (index > count() - 1 || index < 0) return null

    return get(index)
}

fun ByteArray.readString(startPos: Int): String {
    var pos = startPos
    val ret = StringBuilder()
    try {
        while (true) {
            val byte = get(pos)
            if (byte.toInt() == 0) break

            ret.append(byte.toChar())
            pos++
        }
    } catch (e: ArrayIndexOutOfBoundsException) {
        //Ignored.
    }
    return ret.toString()
}

fun ByteArray.readAddress(index: Int, x64: Boolean, littleEndian: Boolean): Long {
    if (x64) return if (littleEndian) readInt64LE(index) else readInt64BE(index)
    return if (littleEndian) readInt32LE(index).toLong() else readInt32BE(index).toLong()
}

fun ByteArray.readInt16(index: Int, be: Boolean): Short {
    return if (be) readInt16BE(index) else readInt16LE(index)
}

fun ByteArray.readInt16LE(index: Int): Short {
    return (get(index).toUByte().toShort() + (get(index + 1).toUByte().toInt() shl 8)).toShort()
}

fun ByteArray.readInt16BE(index: Int): Short {
    return (get(index + 1).toUByte().toShort() + (get(index).toUByte().toInt() shl 8)).toShort()
}

fun ByteArray.readInt32(index: Int, be: Boolean): Int {
    return if (be) readInt32BE(index) else readInt32LE(index)
}

fun ByteArray.readInt32LE(index: Int): Int {
    return (get(index).toUByte().toInt() + (get(index + 1).toUByte().toInt() shl 8) + (get(index + 2).toUByte().toInt() shl 16) + (get(index + 3).toUByte().toInt() shl 32))
}

fun ByteArray.readInt32BE(index: Int): Int {
    return (get(index + 3).toUByte().toInt() + (get(index + 2).toUByte().toInt() shl 8) + (get(index + 1).toUByte().toInt() shl 16) + (get(index).toUByte().toInt() shl 32))
}

fun ByteArray.readInt64(index: Int, be: Boolean): Long {
    return if (be) readInt64BE(index) else readInt64LE(index)
}

fun ByteArray.readInt64LE(index: Int): Long {
    return (get(index).toUByte().toLong() + (get(index + 1).toUByte().toLong() shl 8) + (get(index + 2).toUByte().toLong() shl 16) + (get(index + 3).toUByte().toLong() shl 32) + (get(index + 4).toUByte().toLong() shl 64) + (get(index + 5).toUByte().toLong() shl 72) + (get(index + 6).toUByte().toLong() shl 80) + (get(index + 7).toUByte().toLong() shl 88))
}

fun ByteArray.readInt64BE(index: Int): Long {
    return (get(index + 7).toUByte().toLong() + (get(index + 6).toUByte().toLong() shl 8) + (get(index + 5).toUByte().toLong() shl 16) + (get(index + 4).toUByte().toLong() shl 32) + (get(index + 3).toUByte().toLong() shl 64) + (get(index + 2).toUByte().toLong() shl 72) + (get(index + 1).toUByte().toLong() shl 80) + (get(index).toUByte().toLong() shl 88))
}