package leetcode

/**
 *
 * Created by 98620 on 2018/10/20.
 *
 *
 */

fun main(args: Array<String>) {
    println(Solution4().findMedianSortedArrays(
            intArrayOf(1,2,2),
            intArrayOf(1,2,3)
    ))
}

class Solution4 {

    fun findMedianSortedArrays(nums1: IntArray, nums2: IntArray): Double {
        //奇数和偶数的判断
        //奇数只需要取中间值
        //偶数的话去需要取n/2和 n/2 + 1来做算术平均
        return if ((nums1.size + nums2.size) % 2 != 0) {
            oddCount(nums1, nums2)
        } else {
            return evenCount(nums1, nums2)
        }
        //再做偶数
    }

    private fun oddCount(nums1: IntArray, nums2: IntArray): Double {

        val medianIndex = (nums1.size + nums2.size) / 2

        println(medianIndex)

        var i = 0
        var j = 0

        //当前是第几个数
        var index = -1
        var endWithFirstArray = false

        if (nums1.isEmpty()) {
            return nums2[medianIndex].toDouble()
        } else if (nums2.isEmpty()) {
            return nums1[medianIndex].toDouble()
        }

        while (index < medianIndex) {
            while (index < medianIndex && i < nums1.size && nums1[i] <= nums2[j]) {
                index++
                i++
                endWithFirstArray = true
            }

            if (i == nums1.size) {
                if (index == medianIndex) {
                    return nums1[i - 1].toDouble()
                } else {
                    return nums2[medianIndex - i].toDouble()  //i = 1, m = 2
                }
            }

            while (j < nums2.size && nums2[j] <= nums1[i] && index < medianIndex) {
                j++
                index++
                endWithFirstArray = false
            }

            if (j == nums2.size) {
                if (index == medianIndex) {
                    return nums2[j - 1].toDouble()
                } else {
                    return nums1[medianIndex - j].toDouble()  //i = 1, m = 2
                }
            }

        }

        if (i == 0) {
            return nums2[medianIndex].toDouble()
        }

        if (j == 0) {
            return nums1[medianIndex].toDouble()
        }


        return if (endWithFirstArray) {
            nums1[i - 1].toDouble()
        } else {
            nums2[j - 1].toDouble()
        }
    }

    private fun evenCount(nums1: IntArray, nums2: IntArray): Double {
        val firstNumIndex = (nums1.size + nums2.size) / 2  - 1

        if (nums1.isEmpty()) {
            return (nums2[firstNumIndex] +  nums2[firstNumIndex + 1])/2.toDouble()
        } else if (nums2.isEmpty()) {
            return (nums1[firstNumIndex] +  nums1[firstNumIndex + 1])/2.toDouble()
        }

        var i = 0
        var j = 0

        var firstNumber = 0
        var secondNumber = 0
        //当前是第几个数
        var index = -1
        var endWithFirstArray = false
        while (index < firstNumIndex) {
            while (index < firstNumIndex && i < nums1.size && nums1[i] <= nums2[j]) {
                index++
                i++
                endWithFirstArray = true
            }



            if (i == nums1.size) {
                if (index == firstNumIndex) {
                    return (nums1[i - 1].toDouble() + nums2[j])/2
                } else {
                    return (nums2[firstNumIndex - i] + nums2[firstNumIndex - i + 1]) / 2.toDouble()  //i = 1, m = 2
                }
            } else {
                if (index == firstNumIndex) {
                    firstNumber = nums1[i - 1]
                    secondNumber = if (nums1[i]  < nums2[j]) {
                        nums1[i]
                    } else{
                        nums2[j]
                    }
                    return (firstNumber+ secondNumber)/2.toDouble()
                }
            }

            while (j < nums2.size && nums2[j] <= nums1[i] && index < firstNumIndex) {
                j++
                index++
                endWithFirstArray = false
            }

            if (j == nums2.size) {
                if (index == firstNumIndex) {
                    return (nums1[i].toDouble() + nums2[j - 1])/2
                } else {
                    return (nums1[firstNumIndex - j] + nums1[firstNumIndex - j + 1]) / 2.toDouble()  //i = 1, m = 2
                }
            } else {
                if (index == firstNumIndex) {
                    firstNumber = nums2[j - 1]

                    secondNumber = if (nums2[j] < nums1[i]) {

                        nums2[j]
                    } else{
                        nums1[i]
                    }

                    return (firstNumber+ secondNumber)/2.toDouble()
                }
            }

        }

        if (i == 0) {
            return nums2[firstNumIndex].toDouble()
        }

        if (j == 0) {
            return nums1[firstNumIndex].toDouble()
        }


        return if (endWithFirstArray) {
            nums1[i - 1].toDouble()
        } else {
            nums2[j - 1].toDouble()
        }
    }
}