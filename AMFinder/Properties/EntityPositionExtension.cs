using Vintagestory.API.Common.Entities;

namespace AMFinder.Properties
{
    public static class EntityPositionExtension
    {
        public static EntityPos Subtract(this EntityPos positionThis, EntityPos position)
        {
            positionThis.X -= position.X;
            positionThis.Y -= position.Y;
            positionThis.Z -= position.Z;
            return positionThis;
        }
    }
}