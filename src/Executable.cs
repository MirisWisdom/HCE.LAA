/**
 * Copyright (C) 2023 Emilian Roman / Miris Wisdom
 * 
 * This file is part of HCE.LAA.
 * 
 * HCE.LAA is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 2 of the License, or
 * (at your option) any later version.
 * 
 * HCE.LAA is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with HCE.LAA.  If not, see <http://www.gnu.org/licenses/>.
 */

using System.IO;
using System.IO.Compression;
using System.Text.Json.Serialization;
using static System.Console;
using static System.DateTime;
using static System.IO.Compression.CompressionMode;

namespace Miris.HCE.LAA
{
  public class Executable
  {
    [JsonPropertyName("file")]  public string File  { get; set; }
    [JsonPropertyName("hash")]  public uint   Hash  { get; set; }
    [JsonPropertyName("patch")] public Patch  Patch { get; set; }

    public void CacheExe()
    {
      CacheExe(new FileInfo($"{File}.{$"{Now:s}".Replace(':', '-')}.gz"));
    }

    public void CacheExe(FileInfo archive)
    {
      var fileInfo = new FileInfo(File);

      WriteLine($"CACHE '{fileInfo.Name}' > '{archive.Name}'");

      byte[] b;

      using (FileStream f = new(fileInfo.FullName, FileMode.Open))
      {
        b = new byte[f.Length];
        f.Read(b, 0, (int) f.Length);
      }

      using (FileStream f2 = new(archive.FullName, FileMode.Create))
      using (GZipStream gz = new(f2, Compress, false))
      {
        gz.Write(b, 0, b.Length);
      }
    }

    public void ApplyLaa()
    {
      Patch.Apply(this);
    }
  }
}