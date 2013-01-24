﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cxxi.Passes;

namespace Cxxi
{
    /// <summary>
    /// This class is used to build passes that will be run against the AST
    /// that comes from C++.
    /// </summary>
    public class PassBuilder
    {
        public List<TranslationUnitPass> Passes { get; private set; }
        public Library Library { get; private set; }

        public PassBuilder(Library library)
        {
            Passes = new List<TranslationUnitPass>();
            Library = library;
        }

        public void AddPass(TranslationUnitPass pass)
        {
            pass.Library = Library;
            Passes.Add(pass);
        }

        #region Operation Helpers

        public void RenameWithPattern(string pattern, string replacement, RenameTargets targets)
        {
            AddPass(new RegexRenamePass(pattern, replacement, targets));
        }

        public void RemovePrefix(string prefix)
        {
            AddPass(new RegexRenamePass("^" + prefix, String.Empty));
        }

        public void RemovePrefixEnumItem(string prefix)
        {
            AddPass(new RegexRenamePass("^" + prefix, String.Empty, RenameTargets.EnumItem));
        }

        public void RenameDeclsCase(RenameTargets targets, RenameCasePattern pattern)
        {
            AddPass(new CaseRenamePass(targets, pattern));
        }

        #endregion
    }
}
