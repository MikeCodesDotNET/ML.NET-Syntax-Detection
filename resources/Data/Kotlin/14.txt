package com.extnds.algos.sort

/**
 *  References:
 *  @See <a href="http://www.geeksforgeeks.org/quick-sort">Quick Sort GeeksForGeeks</a>
 *  @See <a href="https://en.wikipedia.org/wiki/Quick_sort">Quick Sort Wikipedia</a>
 */
object QuickSort {

    fun sort(array: IntArray,
             lowerIndex : Int = 0,
             higherIndex : Int = array.size - 1) : IntArray {

        if (lowerIndex < higherIndex) {

            val partitionIndex = partition(array, lowerIndex, higherIndex)

            sort(array, lowerIndex, partitionIndex - 1)
            sort(array,partitionIndex + 1, higherIndex)
        }
        return array
    }

    private fun partition(array: IntArray, lowerIndex: Int, higherIndex: Int) : Int {

        var swapPosition = lowerIndex

        for (i in lowerIndex until higherIndex) {

            if (array[i] < array[higherIndex]) {
                val temp = array[i]
                array[i] = array[swapPosition]
                array[swapPosition] = temp
                swapPosition++
            }
        }

        val temp = array[higherIndex]
        array[higherIndex] = array[swapPosition]
        array[swapPosition] = temp
        return swapPosition
    }
}