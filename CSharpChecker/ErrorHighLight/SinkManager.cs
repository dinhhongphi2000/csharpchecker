using Microsoft.VisualStudio.Shell.TableManager;
using System;

namespace CSharpChecker.ErrorHighLight
{
    /// <summary>
    /// Every consumer of data from an <see cref="ITableDataSource"/> provides an <see cref="ITableDataSink"/> to record the changes. We give the consumer
    /// an IDisposable (this object) that they hang on to as long as they are interested in our data (and they Dispose() of it when they are done).
    /// </summary>
    class SinkManager : IDisposable
    {
        private readonly ErrorHighLightProvider _spellingErrorsProvider;
        private readonly ITableDataSink _sink;

        internal SinkManager(ErrorHighLightProvider spellingErrorsProvider, ITableDataSink sink)
        {
            _spellingErrorsProvider = spellingErrorsProvider;
            _sink = sink;

            spellingErrorsProvider.AddSinkManager(this);
        }

        public void Dispose()
        {
            // Called when the person who subscribed to the data source disposes of the cookie (== this object) they were given.
            _spellingErrorsProvider.RemoveSinkManager(this);
        }

        internal void AddErrorChecker(ErrorHighLightChecker spellChecker)
        {
            _sink.AddFactory(spellChecker.Factory);
        }

        internal void RemoveErrorChecker(ErrorHighLightChecker errorChecker)
        {
            _sink.RemoveFactory(errorChecker.Factory);
        }

        internal void UpdateSink()
        {
            _sink.FactorySnapshotChanged(null);
        }
    }
}
