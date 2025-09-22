using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Nayax.Dex.Repository.Entities
{
    public class BaseRepository : IDisposable
    {
        #region Dispose config

        private bool disposed = false;
        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    handle.Dispose();
                }

                disposed = true;
            }
        }

        public BaseRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}
