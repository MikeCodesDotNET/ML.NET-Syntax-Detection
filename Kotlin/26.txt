package com.danielesergio.md.advancedalgorithms.graph.utils

import java.util.*
import kotlin.NoSuchElementException

class BinaryHeapQueue<T> constructor(private val valueMappedToIndex:MutableMap<T,Int>,
                                     private val data:MutableList<Entry<T>>): Queue<BinaryHeapQueue.Companion.Entry<T>>, MutableCollection<BinaryHeapQueue.Companion.Entry<T>> by data{


    init{
        for (i in (data.size / 2 - 1) downTo 0){
            trickleDown(i)
        }
    }

    companion object{

        data class Entry<T>(val value:T, val weight:Long)

        fun <T>newInstance(vararg entries: Entry<T>): BinaryHeapQueue<T> {
            val data = entries.toList().toMutableList()
            val valueMappedToIndex = (data.map { it.value } zip (0 until data.size)).toMap().toMutableMap()

            return BinaryHeapQueue(valueMappedToIndex, data)
        }

        private fun left(index:Int):Int{
            return 2*index + 1
        }

        private fun right(index:Int):Int{
            return 2*index + 2
        }

        private fun parent(index:Int):Int{
            return Math.floor((index - 1.0)/2).toInt()
        }
    }



    override fun element(): Entry<T> {
        if(data.isEmpty()){
            throw NoSuchElementException()
        }
        return data[0]
    }

    override fun remove(): Entry<T> {
        if(data.isEmpty()){
            throw NoSuchElementException()
        }
        return removeMin()
    }

    override fun offer(e: Entry<T>): Boolean {
        return add(e)
    }

    override fun peek(): Entry<T>? {
        return if(data.isEmpty()){
            null
        } else {
            data[0]
        }
    }

    override fun poll(): Entry<T>? {
        return if(data.isEmpty()){
            null
        } else {
            removeMin()
        }
    }


    override fun add(element: Entry<T>):Boolean{
        return if(valueMappedToIndex.containsKey(element.value)){
            decreaseKey(element)
        } else {
            val indexOfX = data.size
            val addResult = data.add(element)
            if (addResult) {
                valueMappedToIndex[element.value] = indexOfX
                bubbleUp(indexOfX)
            }
            addResult
        }
    }


    private fun removeMin(): Entry<T> {
        val min = data[0]
        val last = data.last()
        data[0] = last
        valueMappedToIndex.remove(min.value)
        valueMappedToIndex[last.value] = 0
        data.removeAt(data.size - 1)
        trickleDown(0)
        return min
    }

    private fun trickleDown(i:Int){
        val left = left(i)
        val right = right(i)
        val n = data.size
        var smallest = i
        if (left < n && data[left].weight < data[i].weight){
            smallest = left
        }
        if (right < n && data[right].weight < data[smallest].weight){
            smallest = right
        }
        if (smallest != i){
            swap(i, smallest)
            trickleDown(smallest)
        }
    }

    private fun swap(i1:Int, i2:Int){
        val entry1 = data[i1]
        val entry2 = data[i2]
        val valueOfI1 = data[i1] //O(1)
        val valueOfI2 = data[i2] //O(1)
        data[i1] = entry2
        data[i2] = entry1
        valueMappedToIndex[valueOfI1.value] = i2 //O(1)
        valueMappedToIndex[valueOfI2.value] = i1 //O(1)
    }

    private fun bubbleUp(i:Int){
        var index = i
        var parentIndex = parent(index)
        while(index>0 && data[index].weight < data[parentIndex].weight){
            swap(index,parentIndex)
            index = parentIndex
            parentIndex = parent(index)
        }
    }

    fun decreaseKey(entryWithNewValue: Entry<T>):Boolean{
        val newWeight = entryWithNewValue.weight
        val value = entryWithNewValue.value
        val valueIndex = valueMappedToIndex[value]!!
        val oldWeight = data[valueIndex].weight
        return  if(oldWeight < newWeight){
            false
        } else {
            data[valueIndex] = Entry(value, newWeight)
            valueMappedToIndex[value] = valueIndex
            bubbleUp(valueIndex)
            true
        }
    }

    fun containsKey(value:T):Boolean{
            return valueMappedToIndex.containsKey(value)
    }

    override fun toString(): String {
        return data.toString()
    }
}