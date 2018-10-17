using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace BuildArchitecture
{
    internal sealed class NodeVisitedListener : CSharpParserBaseListener
    {
        private RuleActionContainer _eventList;
        private CompositionContainer _container;

        public NodeVisitedListener()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(NodeVisitedListener).Assembly));
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

        public override void EnterEveryRule([NotNull] ParserRuleContext context)
        {
            base.EnterEveryRule(context);
            try
            {
                _eventList.RaiseAction(context);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
