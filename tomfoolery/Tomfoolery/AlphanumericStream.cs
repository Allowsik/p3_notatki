namespace Tomfoolery;

public class AlphanumericStream : Stream
{
    private Stream _stream;
    
    public AlphanumericStream(Stream stream)
    {
        this._stream = stream;
    }
    
    private static bool IsAlphanumeric(byte b)
    {
        return (b >= '0' && b <= '9') ||
               (b >= 'A' && b <= 'Z') ||
               (b >= 'a' && b <= 'z');
    }
    
    // tutaj
    public override int Read(byte[] buffer, int offset, int count)
    {
        // rozwiazanie tomka
        int read =  _stream.Read(buffer, offset, count);
        int nextAlpha = 0;
        for (int i = offset; i < read + offset; i++)
        {
            char c =  (char)buffer[i];
            if (char.IsLetterOrDigit(c))
            {
                buffer[offset + nextAlpha] = buffer[i];
                nextAlpha++;
            }
        }
        return nextAlpha;
        
        /*byte[] filtered = new byte[count];
        int filteredCount = 0;

        for (int i = 0; i < count; i++)
        {
            byte current = buffer[i + offset];
            if (IsAlphanumeric(current))
            {
                filtered[filteredCount++] = current;
            }
        }
        
        if (filteredCount > 0)
            _stream.Write(filtered, 0, filteredCount);
        
        return filteredCount;*/
        
    }
    
    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotImplementedException();
    }

    public override void Flush()
    {
        _stream.Flush();
    }
    
    public override long Seek(long offset, SeekOrigin origin)
    {
        return _stream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        _stream.SetLength(value);
    }
    
    public override bool CanRead => _stream.CanRead;
    public override bool CanSeek => _stream.CanSeek;
    public override bool CanWrite => _stream.CanWrite;
    public override long Length => _stream.Length;
    public override long Position 
    {
        get => _stream.Position;
        set => _stream.Position = value;
    }
}