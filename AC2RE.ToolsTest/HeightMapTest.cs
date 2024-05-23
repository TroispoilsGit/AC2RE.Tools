using AC2RE.Definitions;
using AC2RE.Tools;
using Moq;
using Xunit;
using System.Numerics;

namespace AC2RE.ToolsTest {
    public class HeightMapTests {
        private readonly Mock<DatReader> mockDatReader;

        public HeightMapTests() {
            mockDatReader = new Mock<DatReader>(@"C:\DatFiles\cell_1.dat");
        }

        [Fact]
        public void GeneratePositions_ReturnsCorrectSizeArray() {
            // Arrange
            var heightMap = new HeightMap { cellReader = mockDatReader.Object };

            // Act
            var result = heightMap.GeneratePositions();

            // Assert
            int expectedSize = 255 * (17 - 1);
            Assert.Equal(expectedSize, result.GetLength(0));
            Assert.Equal(expectedSize, result.GetLength(1));
        }

        [Fact]
        public void GeneratePositions_CellReaderContainsLandBlock() {
            // Arrange
            var cellId = new CellId(0, 0, 0xFF, 0xFF);
            var landBlockId = new DataId(cellId.id);
            mockDatReader.Setup(reader => reader.contains(landBlockId)).Returns(true);
            mockDatReader.Setup(reader => reader.getFileReader(landBlockId)).Returns(MockFileReader().Object);

            var heightMap = new HeightMap { cellReader = mockDatReader.Object };

            // Act
            var result = heightMap.GeneratePositions();

            // Assert
            // Teste qu'il y a des points générés. Le nombre exact de points dépendra de vos données de test.
            Assert.NotNull(result[0, 0]);
        }

        private Mock<AC2Reader> MockFileReader() {
            var mockReader = new Mock<AC2Reader>(/* paramètres du constructeur si nécessaire */);

            // Configurez le mock pour retourner des données valides pour CLandBlockData
            //mockReader.Setup(reader => reader.(It.IsAny<byte[]>())).Returns(new byte[] { /* données simulées */ });

            var mockLandBlockData = new Mock<CLandBlockData>(mockReader.Object);
            // Simulez les hauteurs des blocs de terrain
            mockLandBlockData.Setup(data => data.heights).Returns(new byte[17 * 17].ToList());

            return mockReader;
        }

        [Fact]
        public void GeneratePositions_CellReaderDoesNotContainLandBlock() {
            // Arrange
            var cellId = new CellId(0, 0, 0xFF, 0xFF);
            var landBlockId = new DataId(cellId.id);
            mockDatReader.Setup(reader => reader.contains(landBlockId)).Returns(false);

            var heightMap = new HeightMap { cellReader = mockDatReader.Object };

            // Act
            var result = heightMap.GeneratePositions();

            // Assert
            // Teste que la position (0,0) n'est pas définie
            Assert.Null(result[0, 0]);
        }
    }
}
