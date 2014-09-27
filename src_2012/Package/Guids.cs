// Guids.cs
// MUST match guids.h
using System;

namespace CodeTitans.TitanControlPanel
{
    static class GuidList
    {
        public const string guidTitanControlPanelPkgString = "355cb66a-99b0-41c5-afa3-8093bd9011c2";
        public const string guidTitanControlPanelCmdSetString = "88d31eda-a8b9-4a1c-8a0d-973d6bf9f0a0";

        public static readonly Guid guidTitanControlPanelCmdSet = new Guid(guidTitanControlPanelCmdSetString);
    };
}