//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.5
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from D:\UIT\KLTN\CSharpParser\Caculator\Caculator.g4 by ANTLR 4.6.5

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Caculator {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="CaculatorParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.5")]
[System.CLSCompliant(false)]
public interface ICaculatorListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>blank</c>
	/// labeled alternative in <see cref="CaculatorParser.stat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlank([NotNull] CaculatorParser.BlankContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>blank</c>
	/// labeled alternative in <see cref="CaculatorParser.stat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlank([NotNull] CaculatorParser.BlankContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>printExpr</c>
	/// labeled alternative in <see cref="CaculatorParser.stat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrintExpr([NotNull] CaculatorParser.PrintExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>printExpr</c>
	/// labeled alternative in <see cref="CaculatorParser.stat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrintExpr([NotNull] CaculatorParser.PrintExprContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>assign</c>
	/// labeled alternative in <see cref="CaculatorParser.stat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssign([NotNull] CaculatorParser.AssignContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>assign</c>
	/// labeled alternative in <see cref="CaculatorParser.stat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssign([NotNull] CaculatorParser.AssignContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>parens</c>
	/// labeled alternative in <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParens([NotNull] CaculatorParser.ParensContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>parens</c>
	/// labeled alternative in <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParens([NotNull] CaculatorParser.ParensContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>MulDiv</c>
	/// labeled alternative in <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMulDiv([NotNull] CaculatorParser.MulDivContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>MulDiv</c>
	/// labeled alternative in <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMulDiv([NotNull] CaculatorParser.MulDivContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>AddSub</c>
	/// labeled alternative in <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAddSub([NotNull] CaculatorParser.AddSubContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>AddSub</c>
	/// labeled alternative in <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAddSub([NotNull] CaculatorParser.AddSubContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>id</c>
	/// labeled alternative in <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterId([NotNull] CaculatorParser.IdContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>id</c>
	/// labeled alternative in <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitId([NotNull] CaculatorParser.IdContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>int</c>
	/// labeled alternative in <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInt([NotNull] CaculatorParser.IntContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>int</c>
	/// labeled alternative in <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInt([NotNull] CaculatorParser.IntContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CaculatorParser.prog"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProg([NotNull] CaculatorParser.ProgContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CaculatorParser.prog"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProg([NotNull] CaculatorParser.ProgContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CaculatorParser.stat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStat([NotNull] CaculatorParser.StatContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CaculatorParser.stat"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStat([NotNull] CaculatorParser.StatContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpr([NotNull] CaculatorParser.ExprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CaculatorParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpr([NotNull] CaculatorParser.ExprContext context);
}
} // namespace Caculator
