using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchaeoTrack.Interfaces {
    public interface IFileDownloader {
        void DownloadFile( string base64Image );
    }
}
