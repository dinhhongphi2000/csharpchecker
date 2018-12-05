// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using Microsoft.VisualStudio.Text.Differencing;

namespace CSharpChecker.LightBulb
{
    internal class DifferenceViewerPreview : IDisposable
    {
        private IWpfDifferenceViewer _viewer;

        public DifferenceViewerPreview(IWpfDifferenceViewer viewer)
        {
            _viewer = viewer ?? throw new FormatException("Viewer null");
        }

        public IWpfDifferenceViewer Viewer => _viewer;

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            if (_viewer != null && !_viewer.IsClosed)
            {
                _viewer.Close();
            }

            _viewer = null;
        }

        ~DifferenceViewerPreview()
        {
            // make sure we are not leaking diff viewer
            // we can't close the view from finalizer thread since it must be same
            // thread (owner thread) this UI is created.
            if (Environment.HasShutdownStarted)
            {
                return;
            }

        }
    }
}