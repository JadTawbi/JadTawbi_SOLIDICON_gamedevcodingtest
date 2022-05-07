using System;
using System.Runtime.InteropServices;
public static class ByteConverter
{
    public static T GetStruct<T>(byte[] arr) where T : struct
    {
        int size = Marshal.SizeOf(default(T));

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(arr, 0, ptr, size);
        var result = (T)Marshal.PtrToStructure(ptr, typeof(T));
        Marshal.FreeHGlobal(ptr);

        return result;
    }

    public static T[] GetStructs<T>(ReadOnlySpan<byte> arr) where T : struct
    {
        return MemoryMarshal.Cast<byte, T>(arr).ToArray();
    }

    public static SequenceData GetSequenceData(Span<byte> data, int sequenceLength)
    {
        var result = new SequenceData();
        int playerTransformByteLength = Marshal.SizeOf(new PlayerTransform()) * sequenceLength * 22; // 22 players
        int startIndex = 0;
        result.PlayerTransforms = GetStructs<PlayerTransform>(data.Slice(startIndex, playerTransformByteLength));
        startIndex += playerTransformByteLength;

        int balltransformsLength = Marshal.SizeOf(new BallTransform()) * sequenceLength;
        result.BallTransforms = GetStructs<BallTransform>(data.Slice(startIndex, balltransformsLength));
        startIndex += balltransformsLength;

        int playerActionsLength = Marshal.SizeOf(new PlayerActions()) * sequenceLength;
        result.PlayerActions = GetStructs<PlayerActions>(data.Slice(startIndex, playerActionsLength));
        startIndex += playerActionsLength;

        int ballOwnerLength = Marshal.SizeOf(new BallOwner()) * sequenceLength;
        result.BallOwners = GetStructs<BallOwner>(data.Slice(startIndex, ballOwnerLength));
        startIndex += ballOwnerLength;

        result.LeftTeamRoles = data.Slice(startIndex, 11).ToArray();
        startIndex += 11;
        result.RightTeamRoles = data.Slice(startIndex, 11).ToArray();

        return result;

    }

    public static byte[] GetBytes<T>(T[] structAry) where T : struct
    {
        int len = structAry.Length;
        int size = Marshal.SizeOf(default(T));
        byte[] arr = new byte[size * len];
        IntPtr ptr = Marshal.AllocHGlobal(size);
        for (int i = 0; i < len; ++i)
        {
            Marshal.StructureToPtr(structAry[i], ptr, true);
            Marshal.Copy(ptr, arr, i * size, size);
        }
        Marshal.FreeHGlobal(ptr);

        return arr;
    }
}
