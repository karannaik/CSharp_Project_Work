// JoiningTableData.cs
// Using LINQ to perform a join and aggregate data across tables.
using System;
using System.Linq;
using System.Windows.Forms;

namespace JoinQueries
{
    public partial class JoiningTableData : Form
    {
        public JoiningTableData()
        {
            InitializeComponent();
        } // end constructor

        private void JoiningTableData_Load(object sender, EventArgs e)
        {
            // Entity Framework DBContext
            BooksEntities dbcontext =
               new BooksEntities();


            // Get authors and titles of each book who wrote them.
            var authorsAndTitles =
               from book in dbcontext.Titles
               from author in book.Authors
               orderby book.Title1
               select new
               {
                   author.FirstName,
                   author.LastName,
                   book.Title1
               };

            outputTextBox.AppendText("\r\n\r\nTitles and Authors:");

            // Display titles and authors.
            foreach (var element in authorsAndTitles)
            {
                outputTextBox.AppendText(
                   String.Format("\r\n{0} {1} {2}",
                       element.Title1, element.FirstName, element.LastName));
            } // end foreach


            // Get Authors and titles with authors sorted for each title
            var authorsAndTitles2 =
               from book in dbcontext.Titles
               from author in book.Authors
               orderby book.Title1, author.LastName, author.FirstName
               select new
               {
                   author.FirstName,
                   author.LastName,
                   book.Title1
               };

            outputTextBox.AppendText("\r\n\r\nAuthors and titles with authors sorted for each title:");

            // Display titles and authors.
            foreach (var element in authorsAndTitles2)
            {
                outputTextBox.AppendText(
                   String.Format("\r\n{0} {1} {2}",
                       element.Title1, element.FirstName, element.LastName));
            } // end foreach


            // Titles grouped by author.
            var titlesByAuthor =
               from book in dbcontext.Titles
               orderby book.Title1
               select new
               {
                   Name = book.Title1,
                   Titles =
                     from author in book.Authors
                     orderby author.LastName, author.FirstName
                     select new { author.FirstName, author.LastName}
               };
           
            outputTextBox.AppendText("\r\n\r\nAuthors grouped by title:");

            // display authors written by each book title, grouped by title
            foreach (var title in titlesByAuthor)
            {
                outputTextBox.AppendText("\r\n\t" + title.Name + ":");

                foreach (var author in title.Titles)
                {
                    outputTextBox.AppendText("\r\n\t\t" + author.FirstName + " " + author.LastName);
                } // end inner foreach
            } // end outer foreach



            //////////////////////////////////////////////////////////////////////////

            //// get authors and ISBNs of each book they co-authored
            //var authorsAndISBNs =
            //   from author in dbcontext.Authors
            //   from book in author.Titles
            //   orderby author.LastName, author.FirstName
            //   select new { author.FirstName, author.LastName, book.ISBN };

            //outputTextBox.AppendText("Authors and ISBNs:");

            //// display authors and ISBNs in tabular format
            //foreach (var element in authorsAndISBNs)
            //{
            //    outputTextBox.AppendText(
            //       String.Format("\r\n\t{0,-10} {1,-10} {2,-10}",
            //          element.FirstName, element.LastName, element.ISBN));
            //} // end foreach

            // get authors and titles of each book they co-authored
            //var authorsAndTitles =
            //   from book in dbcontext.Titles
            //   from author in book.Authors
            //   orderby author.LastName, author.FirstName, book.Title1
            //   select new
            //   {
            //       author.FirstName,
            //       author.LastName,
            //       book.Title1
            //   };

            //outputTextBox.AppendText("\r\n\r\nAuthors and titles:");

            //// display authors and titles in tabular format
            //foreach (var element in authorsAndTitles)
            //{
            //    outputTextBox.AppendText(
            //       String.Format("\r\n\t{0,-10} {1,-10} {2}",
            //          element.FirstName, element.LastName, element.Title1));
            //} // end foreach



        } // end method JoiningTableData_Load
    } // end class JoiningTableData
} // end namespace JoinQueries

