namespace Coorth {
    public class Asset : Disposable {
        
        // private Guid AssetId;
        //
        // protected ReadOnlyMemory<byte> stream;
        //
        // protected ISerializer serializer;
        //
        // public void Load(ReadOnlySpan<byte> data, ISerializer serializer) {
        //     // var re = data.Slice(0, Marshal.SizeOf<Guid>());
        //     //new Guid()
        //     // var id = new Guid(re);
        // }
        //
        // private static readonly ThreadLocal<byte[]> idBytes = new ThreadLocal<byte[]>(()=>new byte[16]);
        //
        // public static Asset Load(Stream stream, ISerializer serializer) {
        //     var type = serializer.ReadType(stream);
        //     var asset = Activator.CreateInstance(type);
        //     
        //     // var size = Marshal.SizeOf<Guid>();
        //     // var bytes = ArrayPool<byte>.Shared.Rent(size);
        //     var bytes = idBytes.Value;
        //     stream.Read(bytes, 0, 16);
        //     var id = new Guid(bytes);
        //
        //     // ArrayPool<byte>.Shared.Return(bytes);
        //     // AssetId = new Guid()
        //     return default;
        // }
    }
}