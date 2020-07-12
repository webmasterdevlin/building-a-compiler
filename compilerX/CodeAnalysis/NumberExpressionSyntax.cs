using System.Collections.Generic;

namespace CompilerX.CodeAnalysis
{
    public class NumberExpressionSyntax : ExpressionSyntax
    {
        public SyntaxToken NumberToken { get; }

        public NumberExpressionSyntax(SyntaxToken numberToken) => NumberToken = numberToken;

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }
    }
}