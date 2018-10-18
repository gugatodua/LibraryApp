using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuramAppWPF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            using (var _ctx = new GuramDBEntities())
            {

                _ctx.Database.CommandTimeout = 200;
                comboBox1.DataSource = _ctx.Authors.Select(y => y.Name).ToList();
                comboBox1.SelectedIndex = -1;
            }


            using (var _ctx = new GuramDBEntities())
            {
                _ctx.Database.CommandTimeout = 200;
                comboBox2.DataSource = _ctx.Books.Select(y => y.Name).ToList();
                comboBox2.SelectedIndex = -1;
            }


            using (var _ctx = new GuramDBEntities())
            {
                _ctx.Database.CommandTimeout = 200;
                comboBox3.DataSource = _ctx.Genres.Select(y => y.Name).ToList();
                comboBox3.SelectedIndex = -1;
            }


            using (var _ctx = new GuramDBEntities())
            {
                _ctx.Database.CommandTimeout = 200;
                comboBox4.DataSource = _ctx.Countries.Select(y => y.Name).ToList();
                comboBox4.SelectedIndex = -1;
            }

            //comboBox7.Enabled = false;
            //comboBox6.Enabled = false;
            //comboBox5.Enabled = false;

        }

        


        private void button1_Click(object sender, EventArgs e)
        {

            var name = comboBox1.Text;
            var book = comboBox2.Text;
            var genre = comboBox3.Text;
            var country = comboBox4.Text;
            var inputYear = comboBox7.Text;
            var inputStartDate = comboBox5.Text;
            var inputEndDate = comboBox6.Text;

            int yearResult = 0;
            int? year = null;

            if (comboBox7.Text == string.Empty)
            {
                year = null;
            }
            else
            {
                if (Int32.TryParse(comboBox7.Text, out yearResult))
                {
                    year = yearResult;
                }
                else throw new Exception("გთხოვთ შეიყვანოთ სწორი წელი");
            }



            int startDateResult = 0;
            int? startDate = null;

            if (inputStartDate == string.Empty)
            {
                startDate = null;
            }
            else
            {
                if (Int32.TryParse(inputStartDate, out startDateResult))
                {
                    startDate = startDateResult;
                }
                else throw new Exception("გთხოვთ შეიყვანოთ სწორი წელი");
            }



            int endDateResult = 0;
            int? endDate = null;

            if (inputEndDate == string.Empty)
            {
                endDate = null;
            }
            else
            {
                if (Int32.TryParse(inputEndDate, out endDateResult))
                {
                    endDate = endDateResult;
                }
                else throw new Exception("გთხოვთ შეიყვანოთ სწორი წელი");
            }

            #region
            //void Disabler()
            //{
            //    if (comboBox1.Text == string.Empty)
            //    {
            //        comboBox1.Enabled = false;
            //    }
            //    if (comboBox2.Text == string.Empty)
            //    {
            //        comboBox2.Enabled = false;
            //    }
            //    if (comboBox2.Text == string.Empty)
            //    {
            //        comboBox2.Enabled = false;
            //    }
            //    if (comboBox3.Text == string.Empty)
            //    {
            //        comboBox3.Enabled = false;
            //    }
            //    if (comboBox4.Text == string.Empty)
            //    {
            //        comboBox4.Enabled = false;
            //    }
            //    if (comboBox5.Text == string.Empty)
            //    {
            //        comboBox5.Enabled = false;
            //    }
            //    if (comboBox6.Text == string.Empty)
            //    {
            //        comboBox6.Enabled = false;
            //    }
            //    if (comboBox7.Text == string.Empty)
            //    {
            //        comboBox7.Enabled = false;
            //    }

            //}


            //void Enabler()
            //{
            //    comboBox1.Enabled = true;
            //    comboBox2.Enabled = true;
            //    comboBox3.Enabled = true;
            //    comboBox4.Enabled = true;
            //    comboBox5.Enabled = true;
            //    comboBox6.Enabled = true;
            //    comboBox7.Enabled = true;
            //}
            #endregion


            var specificBookForAuthor = new List<Book>();

            using (var _context = new GuramDBEntities())
            {
                _context.Database.CommandTimeout = 200;

                var First = name.Split(' ').First();
                var Last = name.Remove(0, First.Length).TrimStart();


                


                specificBookForAuthor = _context.Books.Where(c =>
                                                    (c.Author.Name.Contains(First) || c.Author.Surname.Contains(Last))
                                                    && c.Name.Contains(book)
                                                    && c.Author.Country.Name.Contains(country)
                                                    && c.Genre.Name.Contains(genre)
                                                    //&& inputYear == string.Empty ? true : c.Year == year
                                                    //&& inputStartDate == string.Empty ? true : c.Year >= startDate
                                                    //&& inputEndDate == string.Empty ? true : c.Year <= endDate
                                                    ).Select(b => b).ToList();

                

                //specificBookForAuthor = _context.Books.Where(c =>
                //                                                    (c.Author.Name.Contains(First) || c.Author.Surname.Contains(Last))
                //                                                    && book == string.Empty ? true : c.Name.Contains(book)
                //                                                    && country == string.Empty ? true : c.Author.Country.Name.Contains(country)
                //                                                    && genre == string.Empty ? true : c.Genre.Name.Contains(genre)
                //                                                    //&& inputYear == string.Empty ? true : c.Year == year
                //                                                    //&& inputStartDate == string.Empty ? true : c.Year >= startDate
                //                                                    //&& inputEndDate == string.Empty ? true : c.Year <= endDate
                //                                                    ).Select(b => b).ToList();
                

                richTextBox1.Clear();

                foreach (var item in specificBookForAuthor)
                {

                    richTextBox1.AppendText($"Author :{item.Author.Name} {item.Author.Surname} \nBook : {item.Name}, \nGenre : {item.Genre.Name}, \nCountry : {item.Author.Country.Name} \nYear:{item.Year}  \n----------------------------------------------------\n");
                }

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            richTextBox1.Enabled = true;
        }


        private int MaxAuthorId()
        {
            int output = 0;
            using (GuramDBEntities _ctx = new GuramDBEntities())
            {
                var ids = _ctx.Authors.Select(c => c.Id).ToList();
                var maxId = ids.Count == 0 ? 0 : ids.Max();


                output = maxId + 1;

            }
            return output;
        }


        private int MaxCountryId()
        {
            int output = 0;
            using (GuramDBEntities _ctx = new GuramDBEntities())
            {
                var ids = _ctx.Countries.Select(c => c.Id).ToList();
                var maxId = ids.Count == 0 ? 0 : ids.Max();


                output = maxId + 1;

            }
            return output;
        }

        private int MaxGenreId()
        {
            int output = 0;
            using (GuramDBEntities _ctx = new GuramDBEntities())
            {
                var ids = _ctx.Genres.Select(c => c.Id).ToList();
                var maxId = ids.Count == 0 ? 0 : ids.Max();


                output = maxId + 1;

            }
            return output;
        }

        private int MaxBookId()
        {
            int output = 0;
            using (GuramDBEntities _ctx = new GuramDBEntities())
            {
                var ids = _ctx.Books.Select(c => c.Id).ToList();
                var maxId = ids.Count == 0 ? 0 : ids.Max();


                output = maxId + 1;

            }
            return output;
        }

        //ADD
        private void ADD_Click(object sender, EventArgs e)
        {
            var author = comboBox1.Text;
            var book = comboBox2.Text;
            var genre = comboBox3.Text;
            var country = comboBox4.Text;
            var year = Int32.Parse(comboBox7.Text);

            int? checkauthor;
            int? checkbook;
            int? checkgenre;
            int? checkcountry;


            var First = author.Split(' ').First();
            var Last = author.Remove(0, First.Length).TrimStart();

            var FirstLast = First + Last;

            using (var _ctx = new GuramDBEntities())
            {
                checkauthor = _ctx.Authors.Where(c => (c.Name.ToLower() + c.Surname.ToLower()) == FirstLast.ToLower()).Select(a => a.Id).FirstOrDefault();
                checkbook = _ctx.Books.Where(c => c.Name.ToLower() == book.ToLower()).Select(a => a.Id).FirstOrDefault();
                checkgenre = _ctx.Genres.Where(c => c.Name.ToLower() == genre.ToLower()).Select(a => a.Id).FirstOrDefault();
                checkcountry = _ctx.Countries.Where(c => c.Name.ToLower() == country.ToLower()).Select(a => a.Id).FirstOrDefault();
            }
            Country newCountry = new Country();


            Random rand = new Random();
            if (checkcountry == 0)
            {
                newCountry = new Country()
                {
                    Id = MaxCountryId(),
                    Name = country
                };

                using (var _context = new GuramDBEntities())
                {
                    _context.Countries.Add(newCountry);

                    _context.SaveChanges();
                }

                checkcountry = newCountry.Id;
            }

            Author newAuthor = new Author();

            if (checkauthor == 0)
            {
                newAuthor = new Author()
                {
                    Id = MaxAuthorId(),
                    Name = First,
                    Surname = Last,
                    CountryId = checkcountry
                };

                using (var _context = new GuramDBEntities())
                {
                    _context.Authors.Add(newAuthor);

                    _context.SaveChanges();
                }

                checkauthor = newAuthor.Id;
            }


            Genre newGenre = new Genre();

            if (checkgenre == 0)
            {
                newGenre = new Genre()
                {
                    Id = MaxGenreId(),
                    Name = genre,
                };

                using (var _context = new GuramDBEntities())
                {
                    _context.Genres.Add(newGenre);

                    _context.SaveChanges();
                }

                checkgenre = newGenre.Id;
            }

            Book newBook = new Book();

            if (checkbook == 0)
            {
                newBook = new Book()
                {
                    Id = MaxBookId(),
                    Name = book,
                    GenreId = checkgenre,
                    AuthorId = checkauthor,
                    Year = year
                };

                using (var _context = new GuramDBEntities())
                {
                    _context.Books.Add(newBook);

                    _context.SaveChanges();
                }

                checkbook = newBook.Id;
            }


        }

        //CLEAR
        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox5.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
        }
    }
}
