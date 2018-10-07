using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture
{

    internal sealed class NodeHandlerContainer
    {
        [ImportMany(typeof(Compilation_unitContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Compilation_unitContext { get; set; }

        [ImportMany(typeof(Namespace_or_type_nameContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Namespace_or_type_nameContext { get; set; }

        [ImportMany(typeof(TypeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> TypeContext { get; set; }

        [ImportMany(typeof(Base_typeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Base_typeContext { get; set; }

        [ImportMany(typeof(Simple_typeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Simple_typeContext { get; set; }

        [ImportMany(typeof(Numeric_typeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Numeric_typeContext { get; set; }

        [ImportMany(typeof(Integral_typeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Integral_typeContext { get; set; }

        [ImportMany(typeof(Floating_point_typeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Floating_point_typeContext { get; set; }

        [ImportMany(typeof(Class_typeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Class_typeContext { get; set; }

        [ImportMany(typeof(Type_argument_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Type_argument_listContext { get; set; }

        [ImportMany(typeof(Argument_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Argument_listContext { get; set; }

        [ImportMany(typeof(ArgumentContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> ArgumentContext { get; set; }

        [ImportMany(typeof(ExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> ExpressionContext { get; set; }

        [ImportMany(typeof(Non_assignment_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Non_assignment_expressionContext { get; set; }

        [ImportMany(typeof(AssignmentContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> AssignmentContext { get; set; }

        [ImportMany(typeof(Assignment_operatorContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Assignment_operatorContext { get; set; }

        [ImportMany(typeof(Conditional_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Conditional_expressionContext { get; set; }

        [ImportMany(typeof(Null_coalescing_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Null_coalescing_expressionContext { get; set; }

        [ImportMany(typeof(Conditional_or_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Conditional_or_expressionContext { get; set; }

        [ImportMany(typeof(Conditional_and_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Conditional_and_expressionContext { get; set; }

        [ImportMany(typeof(Inclusive_or_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Inclusive_or_expressionContext { get; set; }

        [ImportMany(typeof(Exclusive_or_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Exclusive_or_expressionContext { get; set; }

        [ImportMany(typeof(And_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> And_expressionContext { get; set; }

        [ImportMany(typeof(Equality_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Equality_expressionContext { get; set; }

        [ImportMany(typeof(Relational_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Relational_expressionContext { get; set; }

        [ImportMany(typeof(Shift_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Shift_expressionContext { get; set; }

        [ImportMany(typeof(Additive_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Additive_expressionContext { get; set; }

        [ImportMany(typeof(Multiplicative_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Multiplicative_expressionContext { get; set; }

        [ImportMany(typeof(Unary_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Unary_expressionContext { get; set; }

        [ImportMany(typeof(Primary_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Primary_expressionContext { get; set; }

        [ImportMany(typeof(Primary_expression_startContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Primary_expression_startContext { get; set; }

        [ImportMany(typeof(LiteralAccessExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> LiteralAccessExpressionContext { get; set; }

        [ImportMany(typeof(DefaultValueExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> DefaultValueExpressionContext { get; set; }

        [ImportMany(typeof(BaseAccessExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> BaseAccessExpressionContext { get; set; }

        [ImportMany(typeof(SizeofExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> SizeofExpressionContext { get; set; }

        [ImportMany(typeof(ParenthesisExpressionsContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> ParenthesisExpressionsContext { get; set; }

        [ImportMany(typeof(ThisReferenceExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> ThisReferenceExpressionContext { get; set; }

        [ImportMany(typeof(ObjectCreationExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> ObjectCreationExpressionContext { get; set; }

        [ImportMany(typeof(AnonymousMethodExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> AnonymousMethodExpressionContext { get; set; }

        [ImportMany(typeof(TypeofExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> TypeofExpressionContext { get; set; }

        [ImportMany(typeof(UncheckedExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> UncheckedExpressionContext { get; set; }

        [ImportMany(typeof(SimpleNameExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> SimpleNameExpressionContext { get; set; }

        [ImportMany(typeof(MemberAccessExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> MemberAccessExpressionContext { get; set; }

        [ImportMany(typeof(CheckedExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> CheckedExpressionContext { get; set; }

        [ImportMany(typeof(LiteralExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> LiteralExpressionContext { get; set; }

        [ImportMany(typeof(NameofExpressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> NameofExpressionContext { get; set; }

        [ImportMany(typeof(Member_accessContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Member_accessContext { get; set; }

        [ImportMany(typeof(Bracket_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Bracket_expressionContext { get; set; }

        [ImportMany(typeof(Indexer_argumentContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Indexer_argumentContext { get; set; }

        [ImportMany(typeof(Predefined_typeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Predefined_typeContext { get; set; }

        [ImportMany(typeof(Expression_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Expression_listContext { get; set; }

        [ImportMany(typeof(Object_or_collection_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Object_or_collection_initializerContext { get; set; }

        [ImportMany(typeof(Object_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Object_initializerContext { get; set; }

        [ImportMany(typeof(Member_initializer_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Member_initializer_listContext { get; set; }

        [ImportMany(typeof(Member_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Member_initializerContext { get; set; }

        [ImportMany(typeof(Initializer_valueContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Initializer_valueContext { get; set; }

        [ImportMany(typeof(Collection_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Collection_initializerContext { get; set; }

        [ImportMany(typeof(Element_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Element_initializerContext { get; set; }

        [ImportMany(typeof(Anonymous_object_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Anonymous_object_initializerContext { get; set; }

        [ImportMany(typeof(Member_declarator_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Member_declarator_listContext { get; set; }

        [ImportMany(typeof(Member_declaratorContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Member_declaratorContext { get; set; }

        [ImportMany(typeof(Unbound_type_nameContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Unbound_type_nameContext { get; set; }

        [ImportMany(typeof(Generic_dimension_specifierContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Generic_dimension_specifierContext { get; set; }

        [ImportMany(typeof(IsTypeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> IsTypeContext { get; set; }

        [ImportMany(typeof(Lambda_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Lambda_expressionContext { get; set; }

        [ImportMany(typeof(Anonymous_function_signatureContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Anonymous_function_signatureContext { get; set; }

        [ImportMany(typeof(Explicit_anonymous_function_parameter_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Explicit_anonymous_function_parameter_listContext { get; set; }

        [ImportMany(typeof(Explicit_anonymous_function_parameterContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Explicit_anonymous_function_parameterContext { get; set; }

        [ImportMany(typeof(Implicit_anonymous_function_parameter_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Implicit_anonymous_function_parameter_listContext { get; set; }

        [ImportMany(typeof(Anonymous_function_bodyContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Anonymous_function_bodyContext { get; set; }

        [ImportMany(typeof(Query_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Query_expressionContext { get; set; }

        [ImportMany(typeof(From_clauseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> From_clauseContext { get; set; }

        [ImportMany(typeof(Query_bodyContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Query_bodyContext { get; set; }

        [ImportMany(typeof(Query_body_clauseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Query_body_clauseContext { get; set; }

        [ImportMany(typeof(Let_clauseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Let_clauseContext { get; set; }

        [ImportMany(typeof(Where_clauseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Where_clauseContext { get; set; }

        [ImportMany(typeof(Combined_join_clauseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Combined_join_clauseContext { get; set; }

        [ImportMany(typeof(Orderby_clauseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Orderby_clauseContext { get; set; }

        [ImportMany(typeof(OrderingContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> OrderingContext { get; set; }

        [ImportMany(typeof(Select_or_group_clauseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Select_or_group_clauseContext { get; set; }

        [ImportMany(typeof(Query_continuationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Query_continuationContext { get; set; }

        [ImportMany(typeof(StatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> StatementContext { get; set; }

        [ImportMany(typeof(DeclarationStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> DeclarationStatementContext { get; set; }

        [ImportMany(typeof(EmbeddedStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> EmbeddedStatementContext { get; set; }

        [ImportMany(typeof(LabeledStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> LabeledStatementContext { get; set; }

        [ImportMany(typeof(Labeled_StatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Labeled_StatementContext { get; set; }

        [ImportMany(typeof(Embedded_statementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Embedded_statementContext { get; set; }

        [ImportMany(typeof(Simple_embedded_statementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Simple_embedded_statementContext { get; set; }

        [ImportMany(typeof(EmptyStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> EmptyStatementContext { get; set; }

        [ImportMany(typeof(TryStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> TryStatementContext { get; set; }

        [ImportMany(typeof(CheckedStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> CheckedStatementContext { get; set; }

        [ImportMany(typeof(ThrowStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> ThrowStatementContext { get; set; }

        [ImportMany(typeof(UnsafeStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> UnsafeStatementContext { get; set; }

        [ImportMany(typeof(ForStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> ForStatementContext { get; set; }

        [ImportMany(typeof(BreakStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> BreakStatementContext { get; set; }

        [ImportMany(typeof(IfStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> IfStatementContext { get; set; }

        [ImportMany(typeof(ReturnStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> ReturnStatementContext { get; set; }

        [ImportMany(typeof(GotoStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> GotoStatementContext { get; set; }

        [ImportMany(typeof(SwitchStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> SwitchStatementContext { get; set; }

        [ImportMany(typeof(FixedStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> FixedStatementContext { get; set; }

        [ImportMany(typeof(WhileStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> WhileStatementContext { get; set; }

        [ImportMany(typeof(DoStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> DoStatementContext { get; set; }

        [ImportMany(typeof(ForeachStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> ForeachStatementContext { get; set; }

        [ImportMany(typeof(UncheckedStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> UncheckedStatementContext { get; set; }

        [ImportMany(typeof(ExpressionStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> ExpressionStatementContext { get; set; }

        [ImportMany(typeof(ContinueStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> ContinueStatementContext { get; set; }

        [ImportMany(typeof(UsingStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> UsingStatementContext { get; set; }

        [ImportMany(typeof(LockStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> LockStatementContext { get; set; }

        [ImportMany(typeof(YieldStatementContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> YieldStatementContext { get; set; }

        [ImportMany(typeof(BlockContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> BlockContext { get; set; }

        [ImportMany(typeof(Local_variable_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Local_variable_declarationContext { get; set; }

        [ImportMany(typeof(Local_variable_typeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Local_variable_typeContext { get; set; }

        [ImportMany(typeof(Local_variable_declaratorContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Local_variable_declaratorContext { get; set; }

        [ImportMany(typeof(Local_variable_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Local_variable_initializerContext { get; set; }

        [ImportMany(typeof(Local_constant_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Local_constant_declarationContext { get; set; }

        [ImportMany(typeof(If_bodyContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> If_bodyContext { get; set; }

        [ImportMany(typeof(Switch_sectionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Switch_sectionContext { get; set; }

        [ImportMany(typeof(Switch_labelContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Switch_labelContext { get; set; }

        [ImportMany(typeof(Statement_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Statement_listContext { get; set; }

        [ImportMany(typeof(For_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> For_initializerContext { get; set; }

        [ImportMany(typeof(For_conditionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> For_conditionContext { get; set; }

        [ImportMany(typeof(For_iteratorContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> For_iteratorContext { get; set; }

        [ImportMany(typeof(Catch_clausesContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Catch_clausesContext { get; set; }

        [ImportMany(typeof(Specific_catch_clauseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Specific_catch_clauseContext { get; set; }

        [ImportMany(typeof(General_catch_clauseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> General_catch_clauseContext { get; set; }

        [ImportMany(typeof(Exception_filterContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Exception_filterContext { get; set; }

        [ImportMany(typeof(Finally_clauseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Finally_clauseContext { get; set; }

        [ImportMany(typeof(Resource_acquisitionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Resource_acquisitionContext { get; set; }

        [ImportMany(typeof(Namespace_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Namespace_declarationContext { get; set; }

        [ImportMany(typeof(NamespaceContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> NamespaceContext { get; set; }

        [ImportMany(typeof(Qualified_identifierContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Qualified_identifierContext { get; set; }

        [ImportMany(typeof(Namespace_bodyContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Namespace_bodyContext { get; set; }

        [ImportMany(typeof(Extern_alias_directivesContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Extern_alias_directivesContext { get; set; }

        [ImportMany(typeof(Extern_alias_directiveContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Extern_alias_directiveContext { get; set; }

        [ImportMany(typeof(Using_directivesContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Using_directivesContext { get; set; }

        [ImportMany(typeof(Using_directiveContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Using_directiveContext { get; set; }

        [ImportMany(typeof(UsingAliasDirectiveContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> UsingAliasDirectiveContext { get; set; }

        [ImportMany(typeof(UsingNamespaceDirectiveContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> UsingNamespaceDirectiveContext { get; set; }

        [ImportMany(typeof(UsingStaticDirectiveContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> UsingStaticDirectiveContext { get; set; }

        [ImportMany(typeof(Namespace_member_declarationsContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Namespace_member_declarationsContext { get; set; }

        [ImportMany(typeof(Namespace_member_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Namespace_member_declarationContext { get; set; }

        [ImportMany(typeof(Type_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Type_declarationContext { get; set; }

        [ImportMany(typeof(Qualified_alias_memberContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Qualified_alias_memberContext { get; set; }

        [ImportMany(typeof(Type_parameter_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Type_parameter_listContext { get; set; }

        [ImportMany(typeof(Type_parameterContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Type_parameterContext { get; set; }

        [ImportMany(typeof(Class_baseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Class_baseContext { get; set; }

        [ImportMany(typeof(Interface_type_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Interface_type_listContext { get; set; }

        [ImportMany(typeof(Type_parameter_constraints_clausesContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Type_parameter_constraints_clausesContext { get; set; }

        [ImportMany(typeof(Type_parameter_constraints_clauseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Type_parameter_constraints_clauseContext { get; set; }

        [ImportMany(typeof(Type_parameter_constraintsContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Type_parameter_constraintsContext { get; set; }

        [ImportMany(typeof(Primary_constraintContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Primary_constraintContext { get; set; }

        [ImportMany(typeof(Secondary_constraintsContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Secondary_constraintsContext { get; set; }

        [ImportMany(typeof(Constructor_constraintContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Constructor_constraintContext { get; set; }

        [ImportMany(typeof(Class_bodyContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Class_bodyContext { get; set; }

        [ImportMany(typeof(Class_member_declarationsContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Class_member_declarationsContext { get; set; }

        [ImportMany(typeof(Class_member_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Class_member_declarationContext { get; set; }

        [ImportMany(typeof(All_member_modifiersContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> All_member_modifiersContext { get; set; }

        [ImportMany(typeof(All_member_modifierContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> All_member_modifierContext { get; set; }

        [ImportMany(typeof(Common_member_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Common_member_declarationContext { get; set; }

        [ImportMany(typeof(Typed_member_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Typed_member_declarationContext { get; set; }

        [ImportMany(typeof(Constant_declaratorsContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Constant_declaratorsContext { get; set; }

        [ImportMany(typeof(Constant_declaratorContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Constant_declaratorContext { get; set; }

        [ImportMany(typeof(Variable_declaratorsContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Variable_declaratorsContext { get; set; }

        [ImportMany(typeof(Variable_declaratorContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Variable_declaratorContext { get; set; }

        [ImportMany(typeof(Variable_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Variable_initializerContext { get; set; }

        [ImportMany(typeof(Return_typeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Return_typeContext { get; set; }

        [ImportMany(typeof(Member_nameContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Member_nameContext { get; set; }

        [ImportMany(typeof(Method_bodyContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Method_bodyContext { get; set; }

        [ImportMany(typeof(Formal_parameter_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Formal_parameter_listContext { get; set; }

        [ImportMany(typeof(Fixed_parametersContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Fixed_parametersContext { get; set; }

        [ImportMany(typeof(Fixed_parameterContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Fixed_parameterContext { get; set; }

        [ImportMany(typeof(Parameter_modifierContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Parameter_modifierContext { get; set; }

        [ImportMany(typeof(Parameter_arrayContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Parameter_arrayContext { get; set; }

        [ImportMany(typeof(Accessor_declarationsContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Accessor_declarationsContext { get; set; }

        [ImportMany(typeof(Get_accessor_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Get_accessor_declarationContext { get; set; }

        [ImportMany(typeof(Set_accessor_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Set_accessor_declarationContext { get; set; }

        [ImportMany(typeof(Accessor_modifierContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Accessor_modifierContext { get; set; }

        [ImportMany(typeof(Accessor_bodyContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Accessor_bodyContext { get; set; }

        [ImportMany(typeof(Event_accessor_declarationsContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Event_accessor_declarationsContext { get; set; }

        [ImportMany(typeof(Add_accessor_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Add_accessor_declarationContext { get; set; }

        [ImportMany(typeof(Remove_accessor_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Remove_accessor_declarationContext { get; set; }

        [ImportMany(typeof(Overloadable_operatorContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Overloadable_operatorContext { get; set; }

        [ImportMany(typeof(Conversion_operator_declaratorContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Conversion_operator_declaratorContext { get; set; }

        [ImportMany(typeof(Constructor_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Constructor_initializerContext { get; set; }

        [ImportMany(typeof(BodyContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> BodyContext { get; set; }

        [ImportMany(typeof(Struct_interfacesContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Struct_interfacesContext { get; set; }

        [ImportMany(typeof(Struct_bodyContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Struct_bodyContext { get; set; }

        [ImportMany(typeof(Struct_member_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Struct_member_declarationContext { get; set; }

        [ImportMany(typeof(Array_typeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Array_typeContext { get; set; }

        [ImportMany(typeof(Rank_specifierContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Rank_specifierContext { get; set; }

        [ImportMany(typeof(Array_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Array_initializerContext { get; set; }

        [ImportMany(typeof(Variant_type_parameter_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Variant_type_parameter_listContext { get; set; }

        [ImportMany(typeof(Variant_type_parameterContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Variant_type_parameterContext { get; set; }

        [ImportMany(typeof(Variance_annotationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Variance_annotationContext { get; set; }

        [ImportMany(typeof(Interface_baseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Interface_baseContext { get; set; }

        [ImportMany(typeof(Interface_bodyContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Interface_bodyContext { get; set; }

        [ImportMany(typeof(Interface_member_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Interface_member_declarationContext { get; set; }

        [ImportMany(typeof(Interface_accessorsContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Interface_accessorsContext { get; set; }

        [ImportMany(typeof(Enum_baseContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Enum_baseContext { get; set; }

        [ImportMany(typeof(Enum_bodyContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Enum_bodyContext { get; set; }

        [ImportMany(typeof(Enum_member_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Enum_member_declarationContext { get; set; }

        [ImportMany(typeof(Global_attribute_sectionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Global_attribute_sectionContext { get; set; }

        [ImportMany(typeof(Global_attribute_targetContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Global_attribute_targetContext { get; set; }

        [ImportMany(typeof(AttributesContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> AttributesContext { get; set; }

        [ImportMany(typeof(Attribute_sectionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Attribute_sectionContext { get; set; }

        [ImportMany(typeof(Attribute_targetContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Attribute_targetContext { get; set; }

        [ImportMany(typeof(Attribute_listContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Attribute_listContext { get; set; }

        [ImportMany(typeof(AttributeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> AttributeContext { get; set; }

        [ImportMany(typeof(Attribute_argumentContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Attribute_argumentContext { get; set; }

        [ImportMany(typeof(Pointer_typeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Pointer_typeContext { get; set; }

        [ImportMany(typeof(Fixed_pointer_declaratorsContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Fixed_pointer_declaratorsContext { get; set; }

        [ImportMany(typeof(Fixed_pointer_declaratorContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Fixed_pointer_declaratorContext { get; set; }

        [ImportMany(typeof(Fixed_pointer_initializerContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Fixed_pointer_initializerContext { get; set; }

        [ImportMany(typeof(Fixed_size_buffer_declaratorContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Fixed_size_buffer_declaratorContext { get; set; }

        [ImportMany(typeof(Local_variable_initializer_unsafeContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Local_variable_initializer_unsafeContext { get; set; }

        [ImportMany(typeof(Right_arrowContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Right_arrowContext { get; set; }

        [ImportMany(typeof(Right_shiftContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Right_shiftContext { get; set; }

        [ImportMany(typeof(Right_shift_assignmentContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Right_shift_assignmentContext { get; set; }

        [ImportMany(typeof(LiteralContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> LiteralContext { get; set; }

        [ImportMany(typeof(Boolean_literalContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Boolean_literalContext { get; set; }

        [ImportMany(typeof(String_literalContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> String_literalContext { get; set; }

        [ImportMany(typeof(Interpolated_regular_stringContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Interpolated_regular_stringContext { get; set; }

        [ImportMany(typeof(Interpolated_verbatium_stringContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Interpolated_verbatium_stringContext { get; set; }

        [ImportMany(typeof(Interpolated_regular_string_partContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Interpolated_regular_string_partContext { get; set; }

        [ImportMany(typeof(Interpolated_verbatium_string_partContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Interpolated_verbatium_string_partContext { get; set; }

        [ImportMany(typeof(Interpolated_string_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Interpolated_string_expressionContext { get; set; }

        [ImportMany(typeof(KeywordContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> KeywordContext { get; set; }

        [ImportMany(typeof(Class_definitionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Class_definitionContext { get; set; }

        [ImportMany(typeof(Struct_definitionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Struct_definitionContext { get; set; }

        [ImportMany(typeof(Interface_definitionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Interface_definitionContext { get; set; }

        [ImportMany(typeof(Enum_definitionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Enum_definitionContext { get; set; }

        [ImportMany(typeof(Delegate_definitionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Delegate_definitionContext { get; set; }

        [ImportMany(typeof(Event_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Event_declarationContext { get; set; }

        [ImportMany(typeof(Field_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Field_declarationContext { get; set; }

        [ImportMany(typeof(Property_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Property_declarationContext { get; set; }

        [ImportMany(typeof(Constant_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Constant_declarationContext { get; set; }

        [ImportMany(typeof(Indexer_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Indexer_declarationContext { get; set; }

        [ImportMany(typeof(Destructor_definitionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Destructor_definitionContext { get; set; }

        [ImportMany(typeof(Constructor_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Constructor_declarationContext { get; set; }

        [ImportMany(typeof(Method_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Method_declarationContext { get; set; }

        [ImportMany(typeof(Method_member_nameContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Method_member_nameContext { get; set; }

        [ImportMany(typeof(Operator_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Operator_declarationContext { get; set; }

        [ImportMany(typeof(Arg_declarationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Arg_declarationContext { get; set; }

        [ImportMany(typeof(Method_invocationContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Method_invocationContext { get; set; }

        [ImportMany(typeof(Object_creation_expressionContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> Object_creation_expressionContext { get; set; }

        [ImportMany(typeof(IdentifierContext))]
        public IEnumerable<Lazy<Action<ParserRuleContext>>> IdentifierContext { get; set; }

        public void RaiseAction(ParserRuleContext context)
        {
            PropertyInfo pro = this.GetType().GetProperty(context.GetType().Name);
            dynamic data = pro.GetValue(this);
            foreach(var x in data)
            {
                var action = x.Value;
                action(context);
            }
        }
    }
}
