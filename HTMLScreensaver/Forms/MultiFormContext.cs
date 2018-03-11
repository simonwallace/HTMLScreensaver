using System.Threading;
using System.Windows.Forms;

namespace HTMLScreensaver.Forms
{
    /// <summary>
    /// Runs multiple forms in a single thread.
    /// Adapted from: https://stackoverflow.com/a/15301089
    /// </summary>
    public class MultiFormContext : ApplicationContext
    {
        /// <summary>The number of forms currently open.</summary>
        private int openFormCount;

        /// <summary>
        /// Runs multiple forms in a single thread.
        /// </summary>
        /// <param name="forms">The forms to run,</param>
        public MultiFormContext(params Form[] forms)
        {
            //Set the initial number of forms currently open
            openFormCount = forms.Length;

            foreach (var form in forms)
            {
                //When a form is closed
                form.FormClosed += (s, args) =>
                {
                    //If all forms have been closed
                    if (Interlocked.Decrement(ref openFormCount) == 0)
                    {
                        //Terminate the thread
                        ExitThread();
                    }
                };

                //Run the form
                form.Show();
            }
        }
    }
}