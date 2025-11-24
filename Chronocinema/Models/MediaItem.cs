using Chronocinema.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chronocinema.Models
{
    public class MediaItem : BaseViewModel
    {
        private int _id;
        private string _title;
        private MediaType _type;
        private WatchingStatus _status;
        private double _rating;
        private string _notes;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string _posterUrl;
        private string _year;
        private string _genre;
        private string _country;
        private string _language;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public MediaType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public WatchingStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public double Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }

        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public string PosterUrl
        {
            get => _posterUrl;
            set => SetProperty(ref _posterUrl, value);
        }

        public string Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }

        public string Genre
        {
            get => _genre;
            set => SetProperty(ref _genre, value);
        }

        public string Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public string Language
        {
            get => _language;
            set => SetProperty(ref _language, value);
        }
    }

    public enum MediaType
    {
        Movie,
        Series
    }

    public enum WatchingStatus
    {
        Completed,
        Watching,
        Planning,
        Paused,
        Dropped
    }
}
