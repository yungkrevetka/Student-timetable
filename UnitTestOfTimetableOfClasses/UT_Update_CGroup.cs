﻿using LibOfTimetableOfClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestOfTimetableOfClasses
{
    [TestClass]
    public class UT_Update_CGroup
    {
        readonly RefData refData = new RefData();
        private void SetupData()
        {
            MDirectionOfPreparation mDirection = new MDirectionOfPreparation("01.03.04", "Прикладная математика", 4);
            Assert.IsTrue(refData.CDirectionOfPreparation.Insert(mDirection),"Не вставить удалить профиль обучения");
 
            MTrainingProfile mTrainingProfile = new MTrainingProfile("Математическое моделирование в экономике и технике", "ММЭТ", "01.03.04");
            Assert.IsTrue(refData.CTrainingProfile.Insert(mTrainingProfile),"Не удалось вставить профиль обучения");
 
            mTrainingProfile = new MTrainingProfile("Математические методы в экономике", "ММЭ", "01.03.04");
            Assert.IsTrue(refData.CTrainingProfile.Insert(mTrainingProfile), "Не удалось вставить направление подготовки");
        }
 
        private void DeleteData()
        {
            MTrainingProfile mTrainingProfile = new MTrainingProfile("Математическое моделирование в экономике и технике", "ММЭТ", "01.03.04");
            Assert.IsTrue(refData.CTrainingProfile.Delete(mTrainingProfile), "Не удалось удалить профиль обучения");
           
            mTrainingProfile = new MTrainingProfile("Математические методы в экономике", "ММЭ", "01.03.04");
            Assert.IsTrue(refData.CTrainingProfile.Delete(mTrainingProfile), "Не удалось удалить профиль обучения");
 
            MDirectionOfPreparation mDirection = new MDirectionOfPreparation("01.03.04", "Прикладная математика", 4);
            Assert.IsTrue(refData.CDirectionOfPreparation.Delete(mDirection), "Не удалось удалить направление подготовки");
        }

        /// <summary>
        /// Изменить сведения в пустой таблице
        /// </summary>
        [TestMethod]
        public void Task_250_1()
        {
            //arrange
            refData.CGroup.Clear();
<<<<<<< HEAD
            Assert.IsTrue(refData.CGroup.Rows.Count == 0, "Не удалось Очистить таблицу группа");
=======
            Assert.IsTrue(refData.CGroup.Rows.Count == 0, "Не удалось очистить таблицу группа");
>>>>>>> f96d5a698448814a2da303de4fb42eb024c9c9d3
            bool expected = true;
            MGroup gr = new MGroup("17-ММбо-2а", 1, "ММЭТ", 1, 1, 0, 0, "Воскресенье");
            expected = false;

            //act
        
            bool actual = refData.CGroup.Update(gr);

            //assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Изменить несуществующую группу в заполненной таблице
        /// </summary>
        [TestMethod]
        public void Task_250_2()
        {
            SetupData();
            //arrange

            MGroup gr = new MGroup("17-ММбо-2а", 1, "ММЭТ", 1, 1, 0, 0, "Воскресенье");
            Assert.IsTrue(refData.CGroup.Insert(gr), "Не удалось вставить группу" +gr.Group );
            bool expected = false;
            //act
            MGroup gr1 = new MGroup("17-ММЭбо-2б", 2, "ММЭ", 2, 2, 1, 1, "Понедельник");
            Assert.IsTrue(refData.CGroup.Insert(gr1), "Не удалось вставить группу" + gr1.Group);

            MGroup gr2 = new MGroup("17-ММЭбо-2в", 2, "ММЭ", 1, 2, 1, 1, "Вторник");

            //act
            gr2.Shift = 2;
            gr2.Students = 3;
            gr2.MaxNumberOfClass = 3;
            gr2.MinNumberOfClass = 4;
            gr2.Weekends = "Суббота";
            bool actual = refData.CGroup.Update(gr2);
            //assert
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(refData.CGroup.Delete(gr), "Не удалось удалить группу" + gr.Group);
            Assert.IsTrue(refData.CGroup.Delete(gr1), "Не удалось удалить группу" + gr1.Group);

            DeleteData();
        }


        /// <summary>
        /// Ввод корректных данных, при условии, что Семестр дублирует Семестр группы  существующего экземпляра
        /// </summary>
       [TestMethod]
        public void Task_250_3() 
        {
            SetupData();
            //arrange
            MGroup gr = new MGroup("17-ММбо-2а", 1, "ММЭТ", 1, 1, 0, 0, "Воскресенье");
            Assert.IsTrue(refData.CGroup.Insert(gr), "Не удалось вставить группу" + gr.Group);

            bool expected = true;
            //act
            MGroup gr1 = new MGroup("17-ММЭбо-2б", 2, "ММЭ", 2, 2, 1, 1, "Воскресенье");
            Assert.IsTrue(refData.CGroup.Insert(gr1), "Не удалось вставить группу" + gr1.Group);

            gr1.Semester = 1;
            bool actual = refData.CGroup.Update(gr1);
            //assert
            Assert.AreEqual(expected, actual);

            Assert.IsTrue(refData.CGroup.Delete(gr), "Не удалось удалить группу" + gr.Group);
            Assert.IsTrue(refData.CGroup.Delete(gr1), "Не удалось удалить группу" + gr1.Group);


            DeleteData();
        }
 
       
        /// <summary>
        /// Ввод корректных данных, при условии, что Смена дублирует Смена существующего экземпляра
        /// </summary>
        [TestMethod]
        public void Task_250_4() 
        {
            SetupData();
            //arrange
            MGroup gr = new MGroup("17-ММбо-2а", 1, "ММЭТ", 1, 1, 0, 0, "Воскресенье");
            Assert.IsTrue(refData.CGroup.Insert(gr), "Не удалось вставить группу" + gr.Group);

            bool expected = true;
            //act
            MGroup gr1 = new MGroup("17-ММЭбо-2б", 2, "ММЭ", 2, 2, 1, 1, "Воскресенье");
            Assert.IsTrue(refData.CGroup.Insert(gr1), "Не удалось вставить группу" + gr1.Group);

            gr1.Shift = 1;
            bool actual = refData.CGroup.Update(gr1);
            //assert
            Assert.AreEqual(expected, actual);

            Assert.IsTrue(refData.CGroup.Delete(gr), "Не удалось удалить группу" + gr.Group);
            Assert.IsTrue(refData.CGroup.Delete(gr1), "Не удалось удалить группу" + gr1.Group);

            DeleteData();
        }
 
        /// <summary>
        /// Ввод корректных данных, при условии, что Студентов дублирует Студентов существующего экземпляра
        /// </summary>
        [TestMethod]
        public void Task_250_5() 
        {
            SetupData();
            //arrange
            MGroup gr = new MGroup("17-ММбо-2а", 1, "ММЭТ", 1, 1, 0, 0, "Воскресенье");
            Assert.IsTrue(refData.CGroup.Insert(gr), "Не удалось вставить группу" + gr.Group);

            bool expected = true;
            //act
            MGroup gr1 = new MGroup("17-ММЭбо-2б", 2, "ММЭ", 2, 2, 1, 1, "Воскресенье");
            Assert.IsTrue(refData.CGroup.Insert(gr1), "Не удалось вставить группу" + gr1.Group);

            gr1.Students = 1;
            bool actual = refData.CGroup.Update(gr1);
            //assert
            Assert.AreEqual(expected, actual);

            Assert.IsTrue(refData.CGroup.Delete(gr), "Не удалось удалить группу" + gr.Group);
            Assert.IsTrue(refData.CGroup.Delete(gr1), "Не удалось удалить группу" + gr1.Group);

            DeleteData();
        }
 
        /// <summary>
        /// Ввод корректных данных, при условии, что График работы дублирует График работы существующего экземпляра
        /// </summary>
        [TestMethod]
        public void Task_250_6() 
        {
            SetupData();
            //arrange
            MGroup gr = new MGroup("17-ММбо-2а", 1, "ММЭТ", 1, 1, 0, 0, "Воскресенье");
            Assert.IsTrue(refData.CGroup.Insert(gr), "Не удалось вставить группу" + gr.Group);

            bool expected = true;
            //act
            MGroup gr1 = new MGroup("17-ММЭбо-2б", 2, "ММЭ", 2, 2, 1, 1, "Воскресенье");
            Assert.IsTrue(refData.CGroup.Insert(gr1), "Не удалось вставить группу" + gr1.Group);

            gr1.MaxNumberOfClass = 0;
            gr1.MinNumberOfClass = 0;
            gr1.Weekends = "Воскресенье";
            bool actual = refData.CGroup.Update(gr1);
            //assert
            Assert.AreEqual(expected, actual);

            Assert.IsTrue(refData.CGroup.Delete(gr), "Не удалось удалить группу" + gr.Group);
            Assert.IsTrue(refData.CGroup.Delete(gr1), "Не удалось удалить группу" + gr1.Group);

            DeleteData();
        }
 
        /// <summary>
        /// Ввод корректных данных при условии что она будет дублировать другую записть(невозможно изменение)
        /// </summary>
        [TestMethod]
        public void Task_250_8() 
        {
            SetupData();
            //arrange
            MGroup gr = new MGroup("17-ММбо-2а", 1, "ММЭТ", 1, 1, 0, 0, "Воскресенье");
            Assert.IsTrue(refData.CGroup.Insert(gr), "Не удалось вставить группу" + gr.Group);

            bool expected = true;
            //act
            MGroup gr1 = new MGroup("17-ММЭбо-2б", 2, "ММЭ", 2, 2, 1, 1, "Воскресенье");
            Assert.IsTrue(refData.CGroup.Insert(gr1), "Не удалось вставить группу" + gr1.Group);

            gr1.Semester = 1;
            gr1.Specialty = "ММЭТ";
            gr1.Shift = 1;
            gr1.Students = 1;
            gr1.MaxNumberOfClass = 0;
            gr1.MinNumberOfClass = 0;
            gr1.Weekends = "Воскресенье";
            bool actual = refData.CGroup.Update(gr1);
            //assert
            Assert.AreEqual(expected, actual);

            Assert.IsTrue(refData.CGroup.Delete(gr), "Не удалось удалить группу" + gr.Group);
            Assert.IsTrue(refData.CGroup.Delete(gr1), "Не удалось удалить группу" + gr1.Group);

            DeleteData();
        }
    }
}
