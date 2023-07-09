//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Misa_TruongWeb03.BL.Service.EmulationTitleService;
//using Misa_TruongWeb03.Common.DTO;
//using Misa_TruongWeb03.Common.Entity.Base;
//using Misa_TruongWeb03.Common.Entity.EmulationTitle;
//using Misa_TruongWeb03.DL.Repository.EmulationTitleRepository;
//using Newtonsoft.Json;
//using NSubstitute;
//using NSubstitute.ReturnsExtensions;
//using System.Reflection;

//namespace Misa_TruongWeb03.UnitTests.Service
//{
//    [TestFixture]
//    public class EmulationTitleTests
//    {
//        #region Property
//        private BaseEntity notFoundEntity = new NotFoundError();
//        private BaseEntity zeroEntity = new DatabaseReturn0Error();
//        private BaseEntity duplicateEntity = new DuplicateError();
//        private BaseEntity validEntity = new BaseEntity
//        {
//            Data = 200,
//            ErrorCode = 200,
//        };
//        #endregion

//        #region Method
//        [Test]
//        public async Task GetAll_ValidInput_ReturnsOk()
//        {
//            //Arrange
//            var emulationTitle = new List<EmulationTitle>();
//            GetEmulationTitle getModel = new GetEmulationTitle();
//            BaseEntity baseEntity = new BaseEntity
//            {
//                Data = emulationTitle
//            };

//            var mapper = Substitute.For<IMapper>();
//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.Get(getModel).Returns(baseEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.Get(getModel);

//            //Assert
//            Assert.That(JsonConvert.SerializeObject(baseEntity) == JsonConvert.SerializeObject(actualResult));
//            await emulationTitleRepository.Received(1).Get(getModel);
//        }
//        [Test]
//        public async Task GetById_NotFound_ReturnsException()
//        {
//            //Arrange
//            var id = 1;

//            var mapper = Substitute.For<IMapper>();
//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.GetById(id).Returns(notFoundEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.GetDetail(id);

//            //Assert
//            Assert.That(actualResult.ErrorCode == StatusCodes.Status404NotFound);
//            await emulationTitleRepository.Received(1).GetById(id);
//        }
//        [Test]
//        public async Task GetById_ValidInput_ReturnsOk()
//        {
//            //Arrange
//            var id = 1;

//            var mapper = Substitute.For<IMapper>();
//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.GetById(id).Returns(validEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.GetDetail(id);

//            //Assert
//            Assert.That(actualResult.ErrorCode == StatusCodes.Status200OK);
//            await emulationTitleRepository.Received(1).GetById(id);
//        }
//        [Test]
//        public async Task Post_ValidInput_ReturnsOk()
//        {
//            //Arrange
//            var newId = 1;
//            PostEmulationTitle postModel = new PostEmulationTitle
//            {
//                EmulationTitleCode = "test"
//            };
//            EmulationTitle checkModel = new EmulationTitle();
//            var mapper = Substitute.For<IMapper>();
//            mapper.Map<EmulationTitle>(postModel).Returns(checkModel);


//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.CheckDuplicate(checkModel).Returns(validEntity);
//            emulationTitleRepository.Post(postModel).Returns(validEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.Post(postModel);

//            //Assert
//            Assert.That(actualResult.ErrorCode == StatusCodes.Status200OK);
//            await emulationTitleRepository.Received(1).CheckDuplicate(checkModel);
//            await emulationTitleRepository.Received(1).Post(postModel);
//        }
//        [Test]
//        public async Task Post_DuplicateCode_ReturnsErrorFound()
//        {
//            //Arrange
//            PostEmulationTitle postModel = new PostEmulationTitle
//            {
//                EmulationTitleCode = "test"
//            };
//            EmulationTitle checkModel = new EmulationTitle();
//            var mapper = Substitute.For<IMapper>();
//            mapper.Map<EmulationTitle>(postModel).Returns(checkModel);


//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.CheckDuplicate(checkModel).Returns(duplicateEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.Post(postModel);

//            //Assert
//            Assert.That(actualResult.ErrorCode == StatusCodes.Status409Conflict);
//            await emulationTitleRepository.Received(1).CheckDuplicate(checkModel);
//            await emulationTitleRepository.Received(0).Post(postModel);
//        }
//        [Test]
//        public async Task Post_DatabaseError_ReturnsDatabaseError()
//        {
//            //Arrange
//            PostEmulationTitle postModel = new PostEmulationTitle
//            {
//                EmulationTitleCode = "test"
//            };
//            EmulationTitle checkModel = new EmulationTitle();
//            var mapper = Substitute.For<IMapper>();
//            mapper.Map<EmulationTitle>(postModel).Returns(checkModel);


//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.CheckDuplicate(checkModel).Returns(validEntity);
//            emulationTitleRepository.Post(postModel).Returns(zeroEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.Post(postModel);

//            //Assert
//            Assert.That(actualResult.Data == null || (int)actualResult.Data == 0);
//            await emulationTitleRepository.Received(1).CheckDuplicate(checkModel);
//            await emulationTitleRepository.Received(1).Post(postModel);
//        }
//        [Test]
//        public async Task Put_ValidInput_ReturnsOk()
//        {
//            //Arrange
//            var id = 1;
//            PostEmulationTitle postModel = new PostEmulationTitle
//            {
//                EmulationTitleCode = "test"
//            };
//            UpdateEmulationTitle putModel = new UpdateEmulationTitle
//            {
//                EmulationTitleCode = "test"
//            };
//            EmulationTitle checkModel = new EmulationTitle();
//            var mapper = Substitute.For<IMapper>();
//            mapper.Map<EmulationTitle>(postModel).Returns(checkModel);
//            mapper.Map<UpdateEmulationTitle>(checkModel).Returns(putModel);

//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.CheckDuplicate(checkModel).Returns(validEntity);
//            emulationTitleRepository.Put(putModel).Returns(validEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.Put(id, postModel);

//            //Assert
//            Assert.That(actualResult.ErrorCode == StatusCodes.Status200OK);
//            await emulationTitleRepository.Received(1).CheckDuplicate(checkModel);
//            await emulationTitleRepository.Received(1).Put(putModel);
//        }
//        [Test]
//        public async Task Put_DuplicateCode_ReturnsErrorFound()
//        {
//            //Arrange
//            var id = 1;
//            PostEmulationTitle postModel = new PostEmulationTitle
//            {
//                EmulationTitleCode = "test"
//            };
//            UpdateEmulationTitle putModel = new UpdateEmulationTitle
//            {
//                EmulationTitleCode = "test"
//            };
//            EmulationTitle checkModel = new EmulationTitle();
//            var mapper = Substitute.For<IMapper>();
//            mapper.Map<EmulationTitle>(postModel).Returns(checkModel);
//            mapper.Map<UpdateEmulationTitle>(checkModel).Returns(putModel);

//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.CheckDuplicate(checkModel).Returns(duplicateEntity);
//            emulationTitleRepository.Put(putModel).Returns(validEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.Put(id, postModel);

//            //Assert
//            Assert.That(actualResult.ErrorCode == StatusCodes.Status409Conflict);
//            await emulationTitleRepository.Received(1).CheckDuplicate(checkModel);
//            await emulationTitleRepository.Received(0).Put(putModel);
//        }
//        [Test]
//        public async Task Put_DatabaseError_ReturnsDatabaseError()
//        {
//            //Arrange
//            var id = 1;
//            PostEmulationTitle postModel = new PostEmulationTitle
//            {
//                EmulationTitleCode = "test"
//            };
//            UpdateEmulationTitle putModel = new UpdateEmulationTitle
//            {
//                EmulationTitleCode = "test"
//            };
//            EmulationTitle checkModel = new EmulationTitle();
//            var mapper = Substitute.For<IMapper>();
//            mapper.Map<EmulationTitle>(postModel).Returns(checkModel);
//            mapper.Map<UpdateEmulationTitle>(checkModel).Returns(putModel);

//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.CheckDuplicate(checkModel).Returns(validEntity);
//            emulationTitleRepository.Put(putModel).Returns(zeroEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.Put(id, postModel);

//            //Assert
//            Assert.That(actualResult.Data == null || (int)actualResult.Data == 0);
//            await emulationTitleRepository.Received(1).CheckDuplicate(checkModel);
//            await emulationTitleRepository.Received(1).Put(putModel);
//        }
//        [Test]
//        public async Task Delete_ValidInput_ReturnsOK()
//        {
//            //Arrange
//            var id = 1;
//            var mapper = Substitute.For<IMapper>();

//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.GetById(id).Returns(validEntity);
//            emulationTitleRepository.Delete(id).Returns(validEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.Delete(id);

//            //Assert
//            Assert.That(actualResult.ErrorCode == StatusCodes.Status200OK);
//            await emulationTitleRepository.Received(1).GetById(id);
//            await emulationTitleRepository.Received(1).Delete(id);
//        }
//        [Test]
//        public async Task Delete_NotFound_ReturnsNotFound()
//        {
//            //Arrange
//            var id = 1;
//            BaseEntity baseEntity = new BaseEntity
//            {
//                ErrorCode = StatusCodes.Status200OK
//            };
//            BaseEntity existEntity = new BaseEntity
//            {
//                Data = null,
//                ErrorCode = StatusCodes.Status404NotFound
//            };
//            EmulationTitle checkModel = new EmulationTitle();
//            var mapper = Substitute.For<IMapper>();

//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.GetById(id).Returns(existEntity);
//            emulationTitleRepository.Delete(id).Returns(baseEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.Delete(id);

//            //Assert
//            Assert.That(actualResult.ErrorCode == StatusCodes.Status404NotFound);
//            await emulationTitleRepository.Received(1).GetById(id);
//            await emulationTitleRepository.Received(0).Delete(id);
//        }
//        [Test]
//        public async Task Delete_DatabaseError_ReturnsDatabaseError()
//        {
//            //Arrange
//            var id = 1;
//            EmulationTitle checkModel = new EmulationTitle();
//            var mapper = Substitute.For<IMapper>();

//            var emulationTitleRepository = Substitute.For<IEmulationTitleRepository>();

//            emulationTitleRepository.GetById(id).Returns(validEntity);
//            emulationTitleRepository.Delete(id).Returns(zeroEntity);

//            var emulationTitleService = new EmulationTitleService(emulationTitleRepository, mapper);

//            //Act
//            var actualResult = await emulationTitleService.Delete(id);

//            //Assert
//            Assert.That(actualResult.Data == null || (int)actualResult.Data == 0);
//            await emulationTitleRepository.Received(1).GetById(id);
//            await emulationTitleRepository.Received(1).Delete(id);
//        } 
//        #endregion
//    }
//}
