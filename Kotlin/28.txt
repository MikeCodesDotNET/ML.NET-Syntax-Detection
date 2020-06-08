package expression.parser

import java.util.*

enum class TokenType {
    LEFT_BR, RIGHT_BR, PLUS, MINUS, MUL, DIV, VARIABLE, CONST, END, ABS, SQUARE
}

data class Token(val type: TokenType, val value: String, val idx: Int)

class InvalidTokenException : ParsingException {
    constructor(msg: String, index: Int) : super("unknown token $msg at position ${index + 1}")

    constructor(symbol: Char, index: Int) : this(symbol.toString(), index)
}

class Tokenizer(expr: String) : Enumeration<Token> {
    private val tokens = ArrayList<Token>()
    private var currToken = -1

    override fun hasMoreElements(): Boolean = currToken < tokens.size - 1

    override fun nextElement(): Token = tokens[++currToken]

    fun current(): Token = tokens[currToken]

    fun prevElement(): Token = tokens[--currToken]

    fun isFirst(): Boolean = currToken == 0

    private fun parse(expr: String) {
        var index = 0
        while (index < expr.length) {
            val symbol = expr[index]
            if (!symbol.isWhitespace()) {
                when (symbol) {
                    '(' -> tokens.add(Token(TokenType.LEFT_BR, "(", index))
                    ')' -> tokens.add(Token(TokenType.RIGHT_BR, ")", index))
                    '+' -> tokens.add(Token(TokenType.PLUS, "+", index))
                    '-' -> tokens.add(Token(TokenType.MINUS, "-", index))
                    '*' -> tokens.add(Token(TokenType.MUL, "*", index))
                    '/' -> tokens.add(Token(TokenType.DIV, "/", index))
                    'x', 'y', 'z' -> tokens.add(Token(TokenType.VARIABLE, symbol.toString(), index))
                    'a' -> index = genToken(expr, "abs", TokenType.ABS, index)
                    's' -> index = genToken(expr, "square", TokenType.SQUARE, index)
                    else -> {
                        if (!symbol.isDigit()) {
                            throw InvalidTokenException(symbol, index)
                        }
                        var end = index
                        while (end < expr.length && expr[end].isDigit()) {
                            end++
                        }
                        val number = expr.substring(index, end)
                        index = end - 1
                        tokens.add(Token(TokenType.CONST, number, index));
                    }
                }
            }
            index++
        }
    }

    private fun genToken(expr: String, token: String, type: TokenType, index: Int): Int {
        if (index + token.length >= expr.length) {
            throw InvalidTokenException(expr.substring(index), index)
        }
        if (expr.substring(index, index + token.length) == token) {
            val j = index + token.length
            if (Character.isAlphabetic(expr[j].toInt())) {
                throw InvalidTokenException(token + expr[j], index)
            }
            tokens.add(Token(type, token, index))
            return index + token.length - 1
        } else {
            var end = index
            while (end < expr.length && !expr[end].isWhitespace()) {
                end++
            }
            throw InvalidTokenException(expr.substring(index, end) + " (expected $token)", index)
        }
    }

    init {
        parse(expr)
    }

}