package edu.unh.cs.ai.omab.utils

import java.lang.Math.min
import java.util.*

/**
 * @author Bence Cserna (bence@cserna.net)
 */


/**
 * Returns the index of largest element or `null` if there are no elements.
 */
fun IntArray.maxIndex(): Int? {
    if (isEmpty()) return null
    var max = this[0]
    var index = 0
    for (i in 1..lastIndex) {
        val e = this[i]
        if (max < e) {
            max = e
            index = i
        }
    }
    return index
}

/**
 * Returns the index of largest element or `null` if there are no elements.
 */
fun DoubleArray.maxIndex(): Int? {
    if (isEmpty()) return null
    var max = this[0]
    var index = 0
    for (i in 1..lastIndex) {
        val e = this[i]
        if (max < e) {
            max = e
            index = i
        }
    }
    return index
}

/**
 * Returns the index of largest element or `null` if there are no elements.
 */
fun DoubleArray.maxIndexAfter(inclusiveIndex: Int): Int? {
    if (isEmpty()) return null
    var max = this[inclusiveIndex]
    var index = inclusiveIndex
    for (i in inclusiveIndex..lastIndex) {
        val e = this[i]
        if (max < e) {
            max = e
            index = i
        }
    }
    return index
}

fun DoubleArray.minIndexBefore(inclusiveIndex: Int): Int? {
    if (isEmpty()) return null
    var min = this[0]
    var index = 0
    for (i in 1..min(lastIndex, inclusiveIndex)) {
        val e = this[i]
        if (min > e) {
            min = e
            index = i
        }
    }

    return index
}

fun DoubleArray.smallerIndicesBefore(exclusiveIndex: Int): ArrayList<Int>? {
    if (isEmpty()) emptyList<Int>()
    val smallerIndices = ArrayList<Int>()
    var reference = this[exclusiveIndex]
    for (i in 0..min(lastIndex, exclusiveIndex - 1)) {
        val e = this[i]
        if (reference > e) {
            smallerIndices.add(i)
        }
    }
    return smallerIndices
}