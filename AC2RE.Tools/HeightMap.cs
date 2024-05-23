using AC2RE.Definitions;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AC2RE.Tools {
    public class HeightMap {
        private const int LANDBLOCK_SIZE = 255;
        private const int BLOCK_SIZE = 17;
        private const int CELL_SIZE = 10;
        private const int GLOBAL_SIZE = LANDBLOCK_SIZE * BLOCK_SIZE * CELL_SIZE;

        public DatReader cellReader;

        public HeightMap() {
            cellReader = new(@"/home/troispoils/Documents/DatFiles/cell_1.dat");
        }

        public Vector3[,] GeneratePositions() {
            var points = new Vector3[LANDBLOCK_SIZE * (BLOCK_SIZE - 1), LANDBLOCK_SIZE * (BLOCK_SIZE - 1)];

            for (byte landy = 0; landy < LANDBLOCK_SIZE; landy++) {
                for (byte landx = 0; landx < LANDBLOCK_SIZE; landx++) {
                    ProcessLandBlock(points, landx, landy);
                }
            }

            return points;
        }

        private void ProcessLandBlock(Vector3[,] points, byte landx, byte landy) {
            var cellId = new CellId(landx, landy, 0xFF, 0xFF);
            var landBlockId = new DataId(cellId.id);

            if (!cellReader.contains(landBlockId))
                return;

            using var data = cellReader.getFileReader(landBlockId);
            var landBlockData = new CLandBlockData(data);

            if (landBlockData == null)
                return;

            PopulatePoints(points, landBlockData, landx, landy);
        }

        private void PopulatePoints(Vector3[,] points, CLandBlockData landBlockData, byte landx, byte landy) {
            for (int y = 0; y < BLOCK_SIZE - 1; y++) {
                for (int x = 0; x < BLOCK_SIZE - 1; x++) {
                    var vectorPoint = CreateVectorPoint(landBlockData, landx, landy, x, y);
                    points[landx * (BLOCK_SIZE - 1) + x, landy * (BLOCK_SIZE - 1) + y] = vectorPoint;
                }
            }
        }

        private Vector3 CreateVectorPoint(CLandBlockData landBlockData, byte landx, byte landy, int x, int y) {
            //TODO: add CELL_SIZE for calculate real pitch or another position
            var pos = x * BLOCK_SIZE + y;
            var posX = landx * BLOCK_SIZE + x - landx;
            var posY = (LANDBLOCK_SIZE * BLOCK_SIZE) - (landy * BLOCK_SIZE + y) - 1 - (LANDBLOCK_SIZE - landy - 1);
            var posZ = landBlockData.heights[pos];

            return new Vector3(posX, posY, posZ);
        }
    }
}
