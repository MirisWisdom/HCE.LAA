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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using static System.Console;
using static System.IO.SeekOrigin;

namespace Miris.HCE.LAA
{
  public class Patch
  {
    [JsonPropertyName("offset")] public uint       Offset { get; set; }
    [JsonPropertyName("data")]   public List<byte> Data   { get; set; }

    public void Apply(Executable executable)
    {
      var fileInfo = new FileInfo(executable.File);

      /* Preserve time-related file metadata. */
      var creationTime   = fileInfo.CreationTime;
      var lastWriteTime  = fileInfo.LastWriteTime;
      var lastAccessTime = fileInfo.LastAccessTime;

      WriteLine($"PATCH [{BitConverter.ToString(Data.ToArray())}] > 0x{Offset:X} > FILE '{fileInfo.Name}'");

      using BinaryWriter bw = new(fileInfo.OpenWrite());
      bw.BaseStream.Seek(Offset, Begin);
      bw.Write(Data.ToArray());

      /* Apply original time-related file metadata. */
      fileInfo.CreationTime   = creationTime;
      fileInfo.LastWriteTime  = lastWriteTime;
      fileInfo.LastAccessTime = lastAccessTime;
    }
  }
}