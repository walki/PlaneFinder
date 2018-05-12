using System;
using Xunit;
using Xunit.Abstractions;

namespace PlaneFinder.Service.Tests
{
    public class PlaneServiceTest
    {

        [Fact]
        public void GetData_ReturnsNonEmptyString()
        {
            // Arrange

            // Act
            string actual = PlaneService.GetData();

            // Assert
            Assert.False(String.IsNullOrWhiteSpace(actual));

        }

        [Fact]
        public void GetData_ReturnsJsonData()
        {
            // Act
            var actual = PlaneService.GetData();
            
            char firstChar = ' ';
            foreach (char ch in actual)
            {
                if (!Char.IsWhiteSpace(ch))
                {
                    firstChar = ch;
                    break;
                }
            }


            // Assert
            Assert.True(firstChar == '{' || firstChar == '[');
        }

        [Fact]
        public void Deserialize_ReturnsOpenSkyResponse_Time()
        {
            // Arrange
            string json = @"{'time':1526079430,'states':[['ab1644','','United States',1526079426,1526079429,-87.8424,42.0282,1013.46,false,118.95,36.03,9.75,null,944.88,'5373',false,0]]}";
            int expected = 1526079430;

            // Act
            var actual = PlaneService.Deserialize(json);
            
            // Assert
            Assert.Equal(actual.Time, expected);

        }

        [Fact]
        public void Deserialize_OpenSkyResponse_GetState_00()
        {

            string json = @"{'time':1526079430,'states':[['ab1644','','United States',1526079426,1526079429,-87.8424,42.0282,1013.46,false,118.95,36.03,9.75,null,944.88,'5373',false,0],['ac96b8','AAL2441 ','United States',1526079429,1526079429,-84.9193,35.5556,11277.6,false,224.61,292.63,0,null,11711.94,'1640',true,0]]}";
            string expected = "ab1644";

            var osr = PlaneService.Deserialize(json);

            Assert.Equal(osr.GetState(0,0), expected);

        }


        [Fact]
        public void Deserialize_OpenSkyResponse_Finds_First_Origin_Country()
        {
            // Arrange

            string json = @"{'time':1526079430,'states':[['ab1644','','United States',1526079426,1526079429,-87.8424,42.0282,1013.46,false,118.95,36.03,9.75,null,944.88,'5373',false,0]]}";
            string expected = "United States";

            // Act
            var osr = PlaneService.Deserialize(json);
            string actual = osr.GetOriginCountry(0);
            
            // Assert
            Assert.Equal(actual, expected);

        }

        [Fact]
        public void Deserialize_OpenSkyResponse_Finds_Second_CallSign()
        {
            // Arrange
            string json = @"{'time':1526079430,'states':[['ab1644','','United States',1526079426,1526079429,-87.8424,42.0282,1013.46,false,118.95,36.03,9.75,null,944.88,'5373',false,0],['ac96b8','AAL2441 ','United States',1526079429,1526079429,-84.9193,35.5556,11277.6,false,224.61,292.63,0,null,11711.94,'1640',true,0]]}";
            string expected = "AAL2441";
           
            // Act
            var osr = PlaneService.Deserialize(json);
            string actual = osr.GetCallSign(1);
            
            // Assert
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Deserialize_OpenSkyResponse_Finds_First_ICAO()
        {
            // Arrange
            string json = @"{'time':1526079430,'states':[['ab1644','','United States',1526079426,1526079429,-87.8424,42.0282,1013.46,false,118.95,36.03,9.75,null,944.88,'5373',false,0],['ac96b8','AAL2441 ','United States',1526079429,1526079429,-84.9193,35.5556,11277.6,false,224.61,292.63,0,null,11711.94,'1640',true,0]]}";
            string expected = "ab1644";
            
            // Act
            var osr = PlaneService.Deserialize(json);
            string actual = osr.GetIcao24(0);
            
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Deserialize_OpenSkyResponse_Finds_SecondBaroAltitude()
        {
            // Arrange 
            string json = @"{'time':1526079430,'states':[['ab1644','','United States',1526079426,1526079429,-87.8424,42.0282,1013.46,false,118.95,36.03,9.75,null,944.88,'5373',false,0],['ac96b8','AAL2441 ','United States',1526079429,1526079429,-84.9193,35.5556,11277.6,false,224.61,292.63,0,null,11711.94,'1640',true,0]]}";
            double expected = 11711.94;
            
            // Act
            var osr = PlaneService.Deserialize(json);
            double? actual = osr.GetBaroAltitude(1);
            
            // Assert
            Assert.Equal(expected, actual);
        }
        

        [Fact]
        public void Calculate_Calculates_EuclideanDistance()
        {
            
            var loc_a = new Location(){Longitude = 48.8584, Latitude = 2.2945};
            var loc_b = new Location(){Longitude = 40.6413, Latitude = -73.7781};

            var distance = new EuclideanDistanceCalculator().Calculate(loc_a, loc_b);

            var expected = 76.5151044119395;
            
            Assert.Equal(expected, distance, 5);
        }

        [Theory]
        [InlineData(48.8584, 2.2945)]
        [InlineData(40.6413, -73.7781)]
        public void FindClosestPlane_ToLocation(double locLong, double locLat)
        {
            var loc = new Location(){Longitude = locLong, Latitude = locLat};

            var data = @"{'time':1526079430,'states':[['ab1644','','United States',1526079426,1526079429,-87.8424,42.0282,1013.46,false,118.95,36.03,9.75,null,944.88,'5373',false,0],['ac96b8','AAL2441 ','United States',1526079429,1526079429,-84.9193,35.5556,11277.6,false,224.61,292.63,0,null,11711.94,'1640',true,0]]}";

            var pf = new  PlaneFinder(data, loc, new EuclideanDistanceCalculator());

            var p = pf.FindClosestPlane();

           // output.WriteLine(p.ToString());

            Assert.Equal("United States", p.CountryOfOrigin);
            Assert.Equal("ac96b8", p.ICao24);

        }

        [Theory]
        [InlineData(1, @"{'time':1526079430,'states':[['ab1644','','United States',1526079426,1526079429,-87.8424,42.0282,1013.46,false,118.95,36.03,9.75,null,944.88,'5373',false,0]]}")]
        [InlineData(2, @"{'time':1526079430,'states':[['ab1644','','United States',1526079426,1526079429,-87.8424,42.0282,1013.46,false,118.95,36.03,9.75,null,944.88,'5373',false,0],['ac96b8','AAL2441 ','United States',1526079429,1526079429,-84.9193,35.5556,11277.6,false,224.61,292.63,0,null,11711.94,'1640',true,0]]}")]
        public void OpenSkyRepsonse_StateCount(int expected, string data)
        {
            var osr = PlaneService.Deserialize(data);

            Assert.Equal(expected, osr.StateCount);
        }


    }
}
