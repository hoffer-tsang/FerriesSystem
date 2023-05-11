/*==============================================================================
 *
 * Provides the entry point for the application.
 *
 * Copyright © Dorset Software, 2015-2017
 *
 *============================================================================*/
using System;
using System.Windows.Forms;

namespace IdentityUserCreator
{
    /// <summary>
    /// Provides the entry point for the application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
