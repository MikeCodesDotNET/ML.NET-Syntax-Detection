package de.th.ma2.graph.v2.adjazenz

// interface for representing a matrix
// this interface is used to only show the relevant parts/fields/methods of the class without overloading with the
// 'complex' implementation
interface AdjazenzMatrix {
    val rowNames: List<String>
    val colNames: List<String>

    fun addRow(rowName: String)
    fun removeRow(row: RowIndex)

    fun addCol(colName: String)
    fun removeCol(col: ColIndex)

    fun row(row: RowIndex): Row
    fun column(col: ColIndex): Column

    // *****************************************************************************************************************
    // utility functions for the code to look more beautiful
    val rows: Iterable<Row>
    val columns: Iterable<Column>

    fun removeRow(rowName: String) = removeRow(rowName.toRowIndex())
    fun removeCol(colName: String) = removeCol(colName.toColIndex())

    operator fun get(row: RowIndex): Row = row(row)
    operator fun get(rowName: String): Row = get(rowName.toRowIndex())
    fun contains(row: RowIndex, col: ColIndex): Boolean = this[row][col]
    fun contains(rowName: String, col: ColIndex): Boolean = contains(rowName.toRowIndex(), col)
    fun contains(row: RowIndex, colName: String): Boolean = contains(row, colName.toColIndex())
    fun contains(rowName: String, colName: String): Boolean = contains(rowName.toRowIndex(), colName.toColIndex())

    fun row(rowName: String): Row = row(rowName.toRowIndex())
    fun column(colName: String): Column = column(colName.toColIndex())

    fun String.toRowIndex(): RowIndex = rowNames.indexOf(this)
    fun String.toColIndex(): ColIndex = colNames.indexOf(this)
}

typealias ColIndex = Int
typealias RowIndex = Int