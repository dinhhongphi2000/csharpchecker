using Microsoft.VisualStudio.Shell.TableManager;

namespace CSharpChecker.ErrorHighLight
{
    class ErrorFactory : TableEntriesSnapshotFactoryBase
    {
        private readonly ErrorHighLightChecker _errorChecker;

        public ErrorSnapShot CurrentSnapshot { get; private set; }

        public ErrorFactory(ErrorHighLightChecker errorChecker, ErrorSnapShot errors)
        {
            _errorChecker = errorChecker;

            this.CurrentSnapshot = errors;
        }

        internal void UpdateErrors(ErrorSnapShot errors)
        {
            this.CurrentSnapshot.NextSnapshot = errors;
            this.CurrentSnapshot = errors;
        }

        #region ITableEntriesSnapshotFactory members
        public override int CurrentVersionNumber
        {
            get
            {
                return this.CurrentSnapshot.VersionNumber;
            }
        }

        public override void Dispose()
        {
        }

        public override ITableEntriesSnapshot GetCurrentSnapshot()
        {
            return this.CurrentSnapshot;
        }

        public override ITableEntriesSnapshot GetSnapshot(int versionNumber)
        {
            // In theory the snapshot could change in the middle of the return statement so snap the snapshot just to be safe.
            var snapshot = this.CurrentSnapshot;
            return (versionNumber == snapshot.VersionNumber) ? snapshot : null;
        }
        #endregion
    }
}
