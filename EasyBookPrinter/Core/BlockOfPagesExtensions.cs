namespace EasyBookPrinter.Core
{
    public static class BlockOfPagesExtensions
    {
        public static BlockOfPages Join(this BlockOfPages block, IEnumerable<BlockOfPages> others)
        {
            foreach (var other in others)
            {
                block = block.Join(other);
            }

            return block;
        }
    }
}
