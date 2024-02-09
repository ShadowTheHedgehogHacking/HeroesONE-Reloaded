﻿using HeroesONE_R.Utilities;
using System.IO;

namespace HeroesONE_R.Structures.Substructures
{
    public class ArchiveFile
    {
        /// <summary>
        /// Stores the individual name of the current file.
        /// </summary>
        public string Name;

        /// <summary>
        /// Stores the RenderWare version of the individual file inside the ONE.
        /// </summary>
        public RWVersion RwVersion;

        /// <summary>
        /// Stores the contents of this individual file.
        /// </summary>
        public byte[] CompressedData;

        /*
            Set of constructors.
            Self explanatory.
        */
        public ArchiveFile()
        { }

        public ArchiveFile(string path)
        {
            byte[] data = File.ReadAllBytes(path);
            Name = Path.GetFileName(path);
            CompressedData = Prs.CompressData(data).ToArray();
            RwVersion.RwVersion = (uint)CommonRWVersions.Heroes;
        }

        public ArchiveFile(string path, RWVersion renderWareVersion) : this(path)
        {
            RwVersion = renderWareVersion;
        }

        public ArchiveFile(string name, byte[] uncompressedData)
        {
            Name = name;
            CompressedData = Prs.CompressData(uncompressedData).ToArray();
            RwVersion.RwVersion = (uint)CommonRWVersions.Heroes;
        }

        public ArchiveFile(string name, byte[] uncompressedData, RWVersion renderWareVersion) : this(name, uncompressedData)
        {
            RwVersion = renderWareVersion;
        }

        /*
            Set of constructors.
            Self explanatory.
        */

        /// <summary>
        /// Returns a copy of the current file that has been PRS Decompressed, ready for writing to disk or manipulation.
        /// </summary>
        public byte[] DecompressThis()
        {
            return Prs.DecompressData(this.CompressedData).ToArray();
        }

        /// <summary>
        /// Writes an uncompressed copy of this individual file to disk.
        /// </summary>
        /// <returns></returns>
        public void WriteToFile(string path)
        {
            File.WriteAllBytes(path, Prs.DecompressData(this.CompressedData).ToArray());
        }
    }
}
