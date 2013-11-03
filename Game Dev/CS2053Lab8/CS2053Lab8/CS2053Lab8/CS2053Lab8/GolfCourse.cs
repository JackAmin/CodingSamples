using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CS2053Lab8
{
    class GolfCourse
    {
        Vector3 origin;
        Vector3 upperLeft;
        Vector3 lowerLeft;
        Vector3 upperRight;
        Vector3 lowerRight;
        Vector3 normal;
        Vector3 up;
        Vector3 left;

        public VertexPositionNormalTexture[] vertices;
        public short[] indexes;

        //The constructor calculates the four corners given the origin, width, height, and facing information supplied by the caller.
        public GolfCourse(Vector3 origin, Vector3 normal, Vector3 up, float width, float height)
        {
            vertices = new VertexPositionNormalTexture[4];
            indexes = new short[6];
            this.origin = origin;
            this.normal = normal;
            this.up = up;

            // Calculate the quad corners
            left = Vector3.Cross(normal, up);
            Vector3 uppercenter = (up * height / 2) + origin;
            upperLeft = uppercenter + (left * width / 2);
            upperRight = uppercenter - (left * width / 2);
            lowerLeft = upperLeft - (up * height);
            lowerRight = upperRight - (up * height);

            fillVertices();
        }

        public float getLeft() {
            return lowerLeft.X;
        }

        public float getRight()
        {
            return upperRight.X;
        }

        public float getTop()
        {
            return upperRight.Z;
        }

        public float getBottom()
        {
            return lowerRight.Z;
        }





        private void fillVertices()
        {
            // Fill in texture coordinates to display full texture on quad
            Vector2 textureUpperLeft = new Vector2(0.0f, 0.0f);
            Vector2 textureUpperRight = new Vector2(1.0f, 0.0f);
            Vector2 textureLowerLeft = new Vector2(0.0f, 1.0f);
            Vector2 textureLowerRight = new Vector2(1.0f, 1.0f);

            // Provide a normal for each vertex
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].Normal = normal;
            }

            // Copy the position and texture coordinate data to vertex array.
            // Set the position and texture coordinate for each vertex
            vertices[0].Position = lowerLeft;
            vertices[0].TextureCoordinate = textureLowerLeft;
            vertices[1].Position = upperLeft;
            vertices[1].TextureCoordinate = textureUpperLeft;
            vertices[2].Position = lowerRight;
            vertices[2].TextureCoordinate = textureLowerRight;
            vertices[3].Position = upperRight;
            vertices[3].TextureCoordinate = textureUpperRight;

            // Drawing two triangles requires four vertices, but six index entries if  using a PrimitiveType.TriangleList.
            // The indices are specified in clockwise order.
            // XNA is a right-handed coordinate system so triangles drawn in counter-clockwise order are
            // assumed to be facing away from the camera which causes them to be culled.
            // Set the index buffer for each vertex, using clockwise winding
            indexes[0] = 0;
            indexes[1] = 1;
            indexes[2] = 2;
            indexes[3] = 2;
            indexes[4] = 1;
            indexes[5] = 3;
        }

    }
}
