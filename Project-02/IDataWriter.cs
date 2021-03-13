using System.Collections;

namespace Project_02 {
    interface IDataWriter {
        /// <summary>
        /// show given IEnumarable of objects.
        /// </summary>
        /// <param name="objects">
        /// IEnumarable of objects to be displayed.
        /// </param>
        public void DisplayObjects(IEnumerable objects);
    }
}
