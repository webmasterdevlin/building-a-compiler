﻿using System.Collections.Generic;

namespace CompilerX.CodeAnalysis
{
    public class Lexer
    {
        private readonly string _text;
        private int _position;
        private readonly List<string> _diagnostics = new List<string>();
        public IEnumerable<string> Diagnostics => _diagnostics;
        private char Current => _position >= _text.Length ? '\0' : _text[_position];

        private void Next() => _position++;

        public Lexer(string text) => _text = text;

        public SyntaxToken NextToken()
        {
            if (_position >= _text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
            
            if (char.IsDigit(Current))
            {
                var start = _position;

                while (char.IsDigit(Current))
                {
                    Next();
                }

                var length = _position - start;
                var text = _text.Substring(start, length);
                if (!int.TryParse(text, out var value))
                    _diagnostics.Add($"The number {_text} isn't a valid Int32");

                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (!char.IsWhiteSpace(Current))
                switch (Current)
                {
                    case '+':
                        return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
                    case '-':
                        return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
                    case '*':
                        return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
                    case '/':
                        return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
                    case '(':
                        return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);
                    case ')':
                        return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);
                    default:
                        _diagnostics.Add($"ERROR: bad character input: '{Current}'");
                        return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1),
                            null);
                }

            {
                var start = _position;

                while (char.IsWhiteSpace(Current))
                {
                    Next();
                }

                var length = _position - start;
                var text = _text.Substring(start, length);
                
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }
        }
    }
}