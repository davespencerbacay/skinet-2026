namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }


        private List<string> _brands = [];
        public List<string> Brands
        {
            get => _brands;
            set => _brands = ParseValues(value);
        }

        // Allow singular query name: ?brand=Angular,React
        public List<string> Brand
        {
            set => Brands = value;
        }

        private List<string> _types = [];
        public List<string> Types
        {
            get => _types;
            set => _types = ParseValues(value);
        }

        // Allow singular query name: ?type=Boots,Gloves
        public List<string> Type
        {
            set => Types = value;
        }

        public string? Sort { get; set; }
        private string? _search;
        public string? Search
        {
            get => _search ?? "";
            set => _search = value?.ToLower();
        }

        private static List<string> ParseValues(List<string> values)
        {
            return values
                .SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

    }
}