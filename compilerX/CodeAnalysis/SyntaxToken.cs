﻿using System.Collections.Generic;
using System.Linq;

namespace CompilerX.CodeAnalysis
{
    public class SyntaxToken : SyntaxNode
    {
        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }

        public override SyntaxKind Kind { get; }
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }

        public int Position { get; }
        private string Text { get; }
        public object Value { get; }
    }
}