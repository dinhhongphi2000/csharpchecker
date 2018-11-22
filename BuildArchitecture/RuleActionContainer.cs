using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Reflection;
using static BuildArchitecture.CSharpParser;

namespace BuildArchitecture
{
    internal sealed class RuleActionContainer
    {
        public delegate void EnterRuleContext(ParserRuleContextWithScope context, out ErrorInformation error);

        [ImportMany(typeof(Compilation_unitContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Compilation_unitContext { get; set; }

        [ImportMany(typeof(Namespace_or_type_nameContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Namespace_or_type_nameContext { get; set; }

        [ImportMany(typeof(TypeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> TypeContext { get; set; }

        [ImportMany(typeof(Base_typeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Base_typeContext { get; set; }

        [ImportMany(typeof(Simple_typeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Simple_typeContext { get; set; }

        [ImportMany(typeof(Numeric_typeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Numeric_typeContext { get; set; }

        [ImportMany(typeof(Integral_typeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Integral_typeContext { get; set; }

        [ImportMany(typeof(Floating_point_typeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Floating_point_typeContext { get; set; }

        [ImportMany(typeof(Class_typeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Class_typeContext { get; set; }

        [ImportMany(typeof(Type_argument_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Type_argument_listContext { get; set; }

        [ImportMany(typeof(Argument_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Argument_listContext { get; set; }

        [ImportMany(typeof(ArgumentContext))]
        public IEnumerable<Lazy<EnterRuleContext>> ArgumentContext { get; set; }

        [ImportMany(typeof(ExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> ExpressionContext { get; set; }

        [ImportMany(typeof(Non_assignment_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Non_assignment_expressionContext { get; set; }

        [ImportMany(typeof(AssignmentContext))]
        public IEnumerable<Lazy<EnterRuleContext>> AssignmentContext { get; set; }

        [ImportMany(typeof(Assignment_operatorContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Assignment_operatorContext { get; set; }

        [ImportMany(typeof(Conditional_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Conditional_expressionContext { get; set; }

        [ImportMany(typeof(Null_coalescing_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Null_coalescing_expressionContext { get; set; }

        [ImportMany(typeof(Conditional_or_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Conditional_or_expressionContext { get; set; }

        [ImportMany(typeof(Conditional_and_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Conditional_and_expressionContext { get; set; }

        [ImportMany(typeof(Inclusive_or_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Inclusive_or_expressionContext { get; set; }

        [ImportMany(typeof(Exclusive_or_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Exclusive_or_expressionContext { get; set; }

        [ImportMany(typeof(And_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> And_expressionContext { get; set; }

        [ImportMany(typeof(Equality_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Equality_expressionContext { get; set; }

        [ImportMany(typeof(Relational_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Relational_expressionContext { get; set; }

        [ImportMany(typeof(Shift_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Shift_expressionContext { get; set; }

        [ImportMany(typeof(Additive_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Additive_expressionContext { get; set; }

        [ImportMany(typeof(Multiplicative_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Multiplicative_expressionContext { get; set; }

        [ImportMany(typeof(Unary_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Unary_expressionContext { get; set; }

        [ImportMany(typeof(Primary_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Primary_expressionContext { get; set; }

        [ImportMany(typeof(Primary_expression_startContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Primary_expression_startContext { get; set; }

        [ImportMany(typeof(LiteralAccessExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> LiteralAccessExpressionContext { get; set; }

        [ImportMany(typeof(DefaultValueExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> DefaultValueExpressionContext { get; set; }

        [ImportMany(typeof(BaseAccessExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> BaseAccessExpressionContext { get; set; }

        [ImportMany(typeof(SizeofExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> SizeofExpressionContext { get; set; }

        [ImportMany(typeof(ParenthesisExpressionsContext))]
        public IEnumerable<Lazy<EnterRuleContext>> ParenthesisExpressionsContext { get; set; }

        [ImportMany(typeof(ThisReferenceExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> ThisReferenceExpressionContext { get; set; }

        [ImportMany(typeof(ObjectCreationExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> ObjectCreationExpressionContext { get; set; }

        [ImportMany(typeof(AnonymousMethodExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> AnonymousMethodExpressionContext { get; set; }

        [ImportMany(typeof(TypeofExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> TypeofExpressionContext { get; set; }

        [ImportMany(typeof(UncheckedExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> UncheckedExpressionContext { get; set; }

        [ImportMany(typeof(SimpleNameExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> SimpleNameExpressionContext { get; set; }

        [ImportMany(typeof(MemberAccessExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> MemberAccessExpressionContext { get; set; }

        [ImportMany(typeof(CheckedExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> CheckedExpressionContext { get; set; }

        [ImportMany(typeof(LiteralExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> LiteralExpressionContext { get; set; }

        [ImportMany(typeof(NameofExpressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> NameofExpressionContext { get; set; }

        [ImportMany(typeof(Member_accessContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Member_accessContext { get; set; }

        [ImportMany(typeof(Bracket_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Bracket_expressionContext { get; set; }

        [ImportMany(typeof(Indexer_argumentContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Indexer_argumentContext { get; set; }

        [ImportMany(typeof(Predefined_typeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Predefined_typeContext { get; set; }

        [ImportMany(typeof(Expression_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Expression_listContext { get; set; }

        [ImportMany(typeof(Object_or_collection_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Object_or_collection_initializerContext { get; set; }

        [ImportMany(typeof(Object_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Object_initializerContext { get; set; }

        [ImportMany(typeof(Member_initializer_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Member_initializer_listContext { get; set; }

        [ImportMany(typeof(Member_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Member_initializerContext { get; set; }

        [ImportMany(typeof(Initializer_valueContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Initializer_valueContext { get; set; }

        [ImportMany(typeof(Collection_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Collection_initializerContext { get; set; }

        [ImportMany(typeof(Element_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Element_initializerContext { get; set; }

        [ImportMany(typeof(Anonymous_object_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Anonymous_object_initializerContext { get; set; }

        [ImportMany(typeof(Member_declarator_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Member_declarator_listContext { get; set; }

        [ImportMany(typeof(Member_declaratorContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Member_declaratorContext { get; set; }

        [ImportMany(typeof(Unbound_type_nameContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Unbound_type_nameContext { get; set; }

        [ImportMany(typeof(Generic_dimension_specifierContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Generic_dimension_specifierContext { get; set; }

        [ImportMany(typeof(IsTypeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> IsTypeContext { get; set; }

        [ImportMany(typeof(Lambda_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Lambda_expressionContext { get; set; }

        [ImportMany(typeof(Anonymous_function_signatureContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Anonymous_function_signatureContext { get; set; }

        [ImportMany(typeof(Explicit_anonymous_function_parameter_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Explicit_anonymous_function_parameter_listContext { get; set; }

        [ImportMany(typeof(Explicit_anonymous_function_parameterContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Explicit_anonymous_function_parameterContext { get; set; }

        [ImportMany(typeof(Implicit_anonymous_function_parameter_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Implicit_anonymous_function_parameter_listContext { get; set; }

        [ImportMany(typeof(Anonymous_function_bodyContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Anonymous_function_bodyContext { get; set; }

        [ImportMany(typeof(Query_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Query_expressionContext { get; set; }

        [ImportMany(typeof(From_clauseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> From_clauseContext { get; set; }

        [ImportMany(typeof(Query_bodyContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Query_bodyContext { get; set; }

        [ImportMany(typeof(Query_body_clauseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Query_body_clauseContext { get; set; }

        [ImportMany(typeof(Let_clauseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Let_clauseContext { get; set; }

        [ImportMany(typeof(Where_clauseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Where_clauseContext { get; set; }

        [ImportMany(typeof(Combined_join_clauseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Combined_join_clauseContext { get; set; }

        [ImportMany(typeof(Orderby_clauseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Orderby_clauseContext { get; set; }

        [ImportMany(typeof(OrderingContext))]
        public IEnumerable<Lazy<EnterRuleContext>> OrderingContext { get; set; }

        [ImportMany(typeof(Select_or_group_clauseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Select_or_group_clauseContext { get; set; }

        [ImportMany(typeof(Query_continuationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Query_continuationContext { get; set; }

        [ImportMany(typeof(StatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> StatementContext { get; set; }

        [ImportMany(typeof(DeclarationStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> DeclarationStatementContext { get; set; }

        [ImportMany(typeof(EmbeddedStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> EmbeddedStatementContext { get; set; }

        [ImportMany(typeof(LabeledStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> LabeledStatementContext { get; set; }

        [ImportMany(typeof(Labeled_StatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Labeled_StatementContext { get; set; }

        [ImportMany(typeof(Embedded_statementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Embedded_statementContext { get; set; }

        [ImportMany(typeof(Simple_embedded_statementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Simple_embedded_statementContext { get; set; }

        [ImportMany(typeof(EmptyStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> EmptyStatementContext { get; set; }

        [ImportMany(typeof(TryStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> TryStatementContext { get; set; }

        [ImportMany(typeof(CheckedStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> CheckedStatementContext { get; set; }

        [ImportMany(typeof(ThrowStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> ThrowStatementContext { get; set; }

        [ImportMany(typeof(UnsafeStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> UnsafeStatementContext { get; set; }

        [ImportMany(typeof(ForStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> ForStatementContext { get; set; }

        [ImportMany(typeof(BreakStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> BreakStatementContext { get; set; }

        [ImportMany(typeof(IfStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> IfStatementContext { get; set; }

        [ImportMany(typeof(ReturnStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> ReturnStatementContext { get; set; }

        [ImportMany(typeof(GotoStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> GotoStatementContext { get; set; }

        [ImportMany(typeof(SwitchStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> SwitchStatementContext { get; set; }

        [ImportMany(typeof(FixedStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> FixedStatementContext { get; set; }

        [ImportMany(typeof(WhileStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> WhileStatementContext { get; set; }

        [ImportMany(typeof(DoStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> DoStatementContext { get; set; }

        [ImportMany(typeof(ForeachStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> ForeachStatementContext { get; set; }

        [ImportMany(typeof(UncheckedStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> UncheckedStatementContext { get; set; }

        [ImportMany(typeof(ExpressionStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> ExpressionStatementContext { get; set; }

        [ImportMany(typeof(ContinueStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> ContinueStatementContext { get; set; }

        [ImportMany(typeof(UsingStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> UsingStatementContext { get; set; }

        [ImportMany(typeof(LockStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> LockStatementContext { get; set; }

        [ImportMany(typeof(YieldStatementContext))]
        public IEnumerable<Lazy<EnterRuleContext>> YieldStatementContext { get; set; }

        [ImportMany(typeof(BlockContext))]
        public IEnumerable<Lazy<EnterRuleContext>> BlockContext { get; set; }

        [ImportMany(typeof(Local_variable_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Local_variable_declarationContext { get; set; }

        [ImportMany(typeof(Local_variable_typeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Local_variable_typeContext { get; set; }

        [ImportMany(typeof(Local_variable_declaratorContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Local_variable_declaratorContext { get; set; }

        [ImportMany(typeof(Local_variable_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Local_variable_initializerContext { get; set; }

        [ImportMany(typeof(Local_constant_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Local_constant_declarationContext { get; set; }

        [ImportMany(typeof(If_bodyContext))]
        public IEnumerable<Lazy<EnterRuleContext>> If_bodyContext { get; set; }

        [ImportMany(typeof(Switch_sectionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Switch_sectionContext { get; set; }

        [ImportMany(typeof(Switch_labelContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Switch_labelContext { get; set; }

        [ImportMany(typeof(Statement_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Statement_listContext { get; set; }

        [ImportMany(typeof(For_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> For_initializerContext { get; set; }

        [ImportMany(typeof(For_conditionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> For_conditionContext { get; set; }

        [ImportMany(typeof(For_iteratorContext))]
        public IEnumerable<Lazy<EnterRuleContext>> For_iteratorContext { get; set; }

        [ImportMany(typeof(Catch_clausesContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Catch_clausesContext { get; set; }

        [ImportMany(typeof(Specific_catch_clauseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Specific_catch_clauseContext { get; set; }

        [ImportMany(typeof(General_catch_clauseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> General_catch_clauseContext { get; set; }

        [ImportMany(typeof(Exception_filterContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Exception_filterContext { get; set; }

        [ImportMany(typeof(Finally_clauseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Finally_clauseContext { get; set; }

        [ImportMany(typeof(Resource_acquisitionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Resource_acquisitionContext { get; set; }

        [ImportMany(typeof(Namespace_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Namespace_declarationContext { get; set; }

        [ImportMany(typeof(NamespaceContext))]
        public IEnumerable<Lazy<EnterRuleContext>> NamespaceContext { get; set; }

        [ImportMany(typeof(Qualified_identifierContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Qualified_identifierContext { get; set; }

        [ImportMany(typeof(Namespace_bodyContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Namespace_bodyContext { get; set; }

        [ImportMany(typeof(Extern_alias_directivesContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Extern_alias_directivesContext { get; set; }

        [ImportMany(typeof(Extern_alias_directiveContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Extern_alias_directiveContext { get; set; }

        [ImportMany(typeof(Using_directivesContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Using_directivesContext { get; set; }

        [ImportMany(typeof(Using_directiveContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Using_directiveContext { get; set; }

        [ImportMany(typeof(UsingAliasDirectiveContext))]
        public IEnumerable<Lazy<EnterRuleContext>> UsingAliasDirectiveContext { get; set; }

        [ImportMany(typeof(UsingNamespaceDirectiveContext))]
        public IEnumerable<Lazy<EnterRuleContext>> UsingNamespaceDirectiveContext { get; set; }

        [ImportMany(typeof(UsingStaticDirectiveContext))]
        public IEnumerable<Lazy<EnterRuleContext>> UsingStaticDirectiveContext { get; set; }

        [ImportMany(typeof(Namespace_member_declarationsContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Namespace_member_declarationsContext { get; set; }

        [ImportMany(typeof(Namespace_member_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Namespace_member_declarationContext { get; set; }

        [ImportMany(typeof(Type_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Type_declarationContext { get; set; }

        [ImportMany(typeof(Qualified_alias_memberContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Qualified_alias_memberContext { get; set; }

        [ImportMany(typeof(Type_parameter_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Type_parameter_listContext { get; set; }

        [ImportMany(typeof(Type_parameterContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Type_parameterContext { get; set; }

        [ImportMany(typeof(Class_baseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Class_baseContext { get; set; }

        [ImportMany(typeof(Interface_type_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Interface_type_listContext { get; set; }

        [ImportMany(typeof(Type_parameter_constraints_clausesContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Type_parameter_constraints_clausesContext { get; set; }

        [ImportMany(typeof(Type_parameter_constraints_clauseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Type_parameter_constraints_clauseContext { get; set; }

        [ImportMany(typeof(Type_parameter_constraintsContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Type_parameter_constraintsContext { get; set; }

        [ImportMany(typeof(Primary_constraintContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Primary_constraintContext { get; set; }

        [ImportMany(typeof(Secondary_constraintsContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Secondary_constraintsContext { get; set; }

        [ImportMany(typeof(Constructor_constraintContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Constructor_constraintContext { get; set; }

        [ImportMany(typeof(Class_bodyContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Class_bodyContext { get; set; }

        [ImportMany(typeof(Class_member_declarationsContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Class_member_declarationsContext { get; set; }

        [ImportMany(typeof(Class_member_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Class_member_declarationContext { get; set; }

        [ImportMany(typeof(All_member_modifiersContext))]
        public IEnumerable<Lazy<EnterRuleContext>> All_member_modifiersContext { get; set; }

        [ImportMany(typeof(All_member_modifierContext))]
        public IEnumerable<Lazy<EnterRuleContext>> All_member_modifierContext { get; set; }

        [ImportMany(typeof(Common_member_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Common_member_declarationContext { get; set; }

        [ImportMany(typeof(Typed_member_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Typed_member_declarationContext { get; set; }

        [ImportMany(typeof(Constant_declaratorsContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Constant_declaratorsContext { get; set; }

        [ImportMany(typeof(Constant_declaratorContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Constant_declaratorContext { get; set; }

        [ImportMany(typeof(Variable_declaratorsContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Variable_declaratorsContext { get; set; }

        [ImportMany(typeof(Variable_declaratorContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Variable_declaratorContext { get; set; }

        [ImportMany(typeof(Variable_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Variable_initializerContext { get; set; }

        [ImportMany(typeof(Return_typeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Return_typeContext { get; set; }

        [ImportMany(typeof(Member_nameContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Member_nameContext { get; set; }

        [ImportMany(typeof(Method_bodyContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Method_bodyContext { get; set; }

        [ImportMany(typeof(Formal_parameter_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Formal_parameter_listContext { get; set; }

        [ImportMany(typeof(Fixed_parametersContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Fixed_parametersContext { get; set; }

        [ImportMany(typeof(Fixed_parameterContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Fixed_parameterContext { get; set; }

        [ImportMany(typeof(Parameter_modifierContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Parameter_modifierContext { get; set; }

        [ImportMany(typeof(Parameter_arrayContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Parameter_arrayContext { get; set; }

        [ImportMany(typeof(Accessor_declarationsContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Accessor_declarationsContext { get; set; }

        [ImportMany(typeof(Get_accessor_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Get_accessor_declarationContext { get; set; }

        [ImportMany(typeof(Set_accessor_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Set_accessor_declarationContext { get; set; }

        [ImportMany(typeof(Accessor_modifierContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Accessor_modifierContext { get; set; }

        [ImportMany(typeof(Accessor_bodyContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Accessor_bodyContext { get; set; }

        [ImportMany(typeof(Event_accessor_declarationsContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Event_accessor_declarationsContext { get; set; }

        [ImportMany(typeof(Add_accessor_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Add_accessor_declarationContext { get; set; }

        [ImportMany(typeof(Remove_accessor_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Remove_accessor_declarationContext { get; set; }

        [ImportMany(typeof(Overloadable_operatorContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Overloadable_operatorContext { get; set; }

        [ImportMany(typeof(Conversion_operator_declaratorContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Conversion_operator_declaratorContext { get; set; }

        [ImportMany(typeof(Constructor_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Constructor_initializerContext { get; set; }

        [ImportMany(typeof(BodyContext))]
        public IEnumerable<Lazy<EnterRuleContext>> BodyContext { get; set; }

        [ImportMany(typeof(Struct_interfacesContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Struct_interfacesContext { get; set; }

        [ImportMany(typeof(Struct_bodyContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Struct_bodyContext { get; set; }

        [ImportMany(typeof(Struct_member_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Struct_member_declarationContext { get; set; }

        [ImportMany(typeof(Array_typeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Array_typeContext { get; set; }

        [ImportMany(typeof(Rank_specifierContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Rank_specifierContext { get; set; }

        [ImportMany(typeof(Array_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Array_initializerContext { get; set; }

        [ImportMany(typeof(Variant_type_parameter_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Variant_type_parameter_listContext { get; set; }

        [ImportMany(typeof(Variant_type_parameterContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Variant_type_parameterContext { get; set; }

        [ImportMany(typeof(Variance_annotationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Variance_annotationContext { get; set; }

        [ImportMany(typeof(Interface_baseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Interface_baseContext { get; set; }

        [ImportMany(typeof(Interface_bodyContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Interface_bodyContext { get; set; }

        [ImportMany(typeof(Interface_member_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Interface_member_declarationContext { get; set; }

        [ImportMany(typeof(Interface_accessorsContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Interface_accessorsContext { get; set; }

        [ImportMany(typeof(Enum_baseContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Enum_baseContext { get; set; }

        [ImportMany(typeof(Enum_bodyContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Enum_bodyContext { get; set; }

        [ImportMany(typeof(Enum_member_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Enum_member_declarationContext { get; set; }

        [ImportMany(typeof(Global_attribute_sectionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Global_attribute_sectionContext { get; set; }

        [ImportMany(typeof(Global_attribute_targetContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Global_attribute_targetContext { get; set; }

        [ImportMany(typeof(AttributesContext))]
        public IEnumerable<Lazy<EnterRuleContext>> AttributesContext { get; set; }

        [ImportMany(typeof(Attribute_sectionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Attribute_sectionContext { get; set; }

        [ImportMany(typeof(Attribute_targetContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Attribute_targetContext { get; set; }

        [ImportMany(typeof(Attribute_listContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Attribute_listContext { get; set; }

        [ImportMany(typeof(AttributeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> AttributeContext { get; set; }

        [ImportMany(typeof(Attribute_argumentContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Attribute_argumentContext { get; set; }

        [ImportMany(typeof(Pointer_typeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Pointer_typeContext { get; set; }

        [ImportMany(typeof(Fixed_pointer_declaratorsContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Fixed_pointer_declaratorsContext { get; set; }

        [ImportMany(typeof(Fixed_pointer_declaratorContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Fixed_pointer_declaratorContext { get; set; }

        [ImportMany(typeof(Fixed_pointer_initializerContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Fixed_pointer_initializerContext { get; set; }

        [ImportMany(typeof(Fixed_size_buffer_declaratorContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Fixed_size_buffer_declaratorContext { get; set; }

        [ImportMany(typeof(Local_variable_initializer_unsafeContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Local_variable_initializer_unsafeContext { get; set; }

        [ImportMany(typeof(Right_arrowContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Right_arrowContext { get; set; }

        [ImportMany(typeof(Right_shiftContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Right_shiftContext { get; set; }

        [ImportMany(typeof(Right_shift_assignmentContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Right_shift_assignmentContext { get; set; }

        [ImportMany(typeof(LiteralContext))]
        public IEnumerable<Lazy<EnterRuleContext>> LiteralContext { get; set; }

        [ImportMany(typeof(Boolean_literalContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Boolean_literalContext { get; set; }

        [ImportMany(typeof(String_literalContext))]
        public IEnumerable<Lazy<EnterRuleContext>> String_literalContext { get; set; }

        [ImportMany(typeof(Interpolated_regular_stringContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Interpolated_regular_stringContext { get; set; }

        [ImportMany(typeof(Interpolated_verbatium_stringContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Interpolated_verbatium_stringContext { get; set; }

        [ImportMany(typeof(Interpolated_regular_string_partContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Interpolated_regular_string_partContext { get; set; }

        [ImportMany(typeof(Interpolated_verbatium_string_partContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Interpolated_verbatium_string_partContext { get; set; }

        [ImportMany(typeof(Interpolated_string_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Interpolated_string_expressionContext { get; set; }

        [ImportMany(typeof(KeywordContext))]
        public IEnumerable<Lazy<EnterRuleContext>> KeywordContext { get; set; }

        [ImportMany(typeof(Class_definitionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Class_definitionContext { get; set; }

        [ImportMany(typeof(Struct_definitionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Struct_definitionContext { get; set; }

        [ImportMany(typeof(Interface_definitionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Interface_definitionContext { get; set; }

        [ImportMany(typeof(Enum_definitionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Enum_definitionContext { get; set; }

        [ImportMany(typeof(Delegate_definitionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Delegate_definitionContext { get; set; }

        [ImportMany(typeof(Event_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Event_declarationContext { get; set; }

        [ImportMany(typeof(Field_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Field_declarationContext { get; set; }

        [ImportMany(typeof(Property_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Property_declarationContext { get; set; }

        [ImportMany(typeof(Constant_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Constant_declarationContext { get; set; }

        [ImportMany(typeof(Indexer_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Indexer_declarationContext { get; set; }

        [ImportMany(typeof(Destructor_definitionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Destructor_definitionContext { get; set; }

        [ImportMany(typeof(Constructor_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Constructor_declarationContext { get; set; }

        [ImportMany(typeof(Method_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Method_declarationContext { get; set; }

        [ImportMany(typeof(Method_member_nameContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Method_member_nameContext { get; set; }

        [ImportMany(typeof(Operator_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Operator_declarationContext { get; set; }

        [ImportMany(typeof(Arg_declarationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Arg_declarationContext { get; set; }

        [ImportMany(typeof(Method_invocationContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Method_invocationContext { get; set; }

        [ImportMany(typeof(Object_creation_expressionContext))]
        public IEnumerable<Lazy<EnterRuleContext>> Object_creation_expressionContext { get; set; }

        [ImportMany(typeof(IdentifierContext))]
        public IEnumerable<Lazy<EnterRuleContext>> IdentifierContext { get; set; }

        public void RaiseAction(ParserRuleContextWithScope context, List<ErrorInformation> errorInformationList)
        {
            PropertyInfo pro = this.GetType().GetProperty(context.GetType().Name);
            dynamic data = pro.GetValue(this);
            foreach(var x in data)
            {
                var action = x.Value;
                ErrorInformation error;
                action(context, out error);
                if(error != null)
                {
                    errorInformationList.Add(error);
                }
            }
        }
    }
}
