using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using BuildArchitecture.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace BuildArchitecture
{
    internal sealed class RuleChecker : CSharpParserBaseListener
    {
        private RuleActionContainer _eventList;
        private CompositionContainer _container;
        private List<ErrorInformation> errorTable;
        private ITokenStream tokenStream;

        public RuleChecker()
        {
            errorTable = new List<ErrorInformation>();
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(RuleChecker).Assembly));
            _container = new CompositionContainer(catalog);
            //Fill the imports of this object
            try
            {
                _eventList = new RuleActionContainer();
                this._container.ComposeParts(_eventList);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        public List<ErrorInformation> GetErrors()
        {
            return errorTable;
        }

        public override void EnterCompilation_unit([NotNull] CSharpParser.Compilation_unitContext context)
        {
            //reset errorTable
            errorTable.Clear();
            base.EnterCompilation_unit(context);
        }

        public void SetCurrentTokenStream(ITokenStream stream)
        {
            tokenStream = stream ?? throw new ArgumentNullException();
            _eventList.UpdateTokenStream(stream);
        }

        public override void EnterEveryRule([NotNull] ParserRuleContext context)
        {
            base.EnterEveryRule(context);
            try
            {
                _eventList.RaiseAction(context, errorTable);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
