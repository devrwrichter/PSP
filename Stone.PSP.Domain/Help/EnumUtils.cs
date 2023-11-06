namespace Stone.PSP.Domain.Help
{
    public static class EnumUtils
    {
        public static bool ExistsInEnum<TEnum>(int value) where TEnum : struct
        {
            var values = Enum.GetValues(typeof(TEnum));
            var result = new List<int>();
            foreach (var v in values)
                result.Add(Convert.ToInt32(v));

            return result.Exists(x => x.Equals(value));
        }
    }
}
