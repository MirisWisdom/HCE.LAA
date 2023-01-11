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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using static System.Console;
using static System.IO.File;

namespace Miris.HCE.LAA
{
  internal static class Program
  {
    private static readonly List<FileInfo> Patches = new()
    {
      new FileInfo("mcc-laa.json")
    };

    private static void Main(string[] args)
    {
      WriteLine(@"               _      _                ");
      WriteLine(@"    ____ ___  (_)____(_)____           ");
      WriteLine(@"   / __ `__ \/ / ___/ / ___/           ");
      WriteLine(@"  / / / / / / / /  / (__  )            ");
      WriteLine(@" /_/ /_/ /_/_/_/  /_/____/             ");
      WriteLine(@"  _      __(_)________/ /___  ____ ___ ");
      WriteLine(@" | | /| / / / ___/ __  / __ \/ __ `__ \");
      WriteLine(@" | |/ |/ / (__  ) /_/ / /_/ / / / / / /");
      WriteLine(@" |__/|__/_/____/\__,_/\____/_/ /_/ /_/ ");
      WriteLine(@"                                       ");
      WriteLine(@"========================================");
      WriteLine(@"LAA Patcher for HCE/SPV3/MCC Executables");
      WriteLine(@"Source :: GitHub.com/MirisWisdom/HCE.LAA");
      WriteLine(@"========================================");

      var found = false;

      /**
       * Executables which:
       *
       * 1. Have a patch defined; and
       * 2. Exist on the filesystem.
       */

      var executables = Patches
        .Select(patch => JsonSerializer.Deserialize<List<Executable>>(ReadAllText(patch.FullName)))
        .Where(executables => executables != null)
        .SelectMany(executables => executables!.Where(executable => Exists(executable.File)));

      foreach (var executable in executables)
      {
        found = true;
        executable.CacheExe();
        executable.ApplyLaa();
      }

      if (found)
      {
        WriteLine("========================================");
        WriteLine("PATCH SUCCESS! PRESS ANY KEY TO CONTINUE");
      }
      else
      {
        WriteLine("NO FILE FOUND! PRESS ANY KEY TO CONTINUE");
      }

      ReadLine();
    }
  }
}