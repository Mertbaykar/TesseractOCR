using System.Collections.ObjectModel;


namespace TesseractOCR.Helpers.Helpers
{
    public class StreamCollection :  IDisposable
    {
        private bool disposedValue;
        
        public List<Stream> Streams { get; set; }

        public StreamCollection()
        {
            Streams = new List<Stream>();
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    if (this.Streams != null && this.Streams.Count>0)
                    {
                        foreach (var stream in this.Streams)
                        {
                            if (stream != null)
                                stream.Dispose();
                        }
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~StreamCollection()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
