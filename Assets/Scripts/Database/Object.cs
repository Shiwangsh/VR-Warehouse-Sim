using System;

[Serializable]

public class Object
{
    public string ObjectId { get; set; }
    public string ObjectShape { get; set; }
    public string ObjectSize { get; set; }

    public Object(string objectId, string objectShape, string objectSize)
    {
        ObjectId = objectId;
        ObjectShape = objectShape;
        ObjectSize = objectSize;
    }
}

