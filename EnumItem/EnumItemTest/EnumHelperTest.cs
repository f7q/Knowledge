using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using EnumItem;

namespace EnumyItem.Test
{
    public class EnumHelperTest
    {
        private EnumHelper helper;
        public EnumHelperTest()
        {
            this.helper = new EnumHelper();
        }
        [Fact]
        public void Test1()
        {
            Assert.Equal("test", this.helper.GetKbn<EnumSample>(EnumSample.Kbn1));
        }
        [Fact]
        public void Test2()
        {
            Assert.Equal("aaaa", this.helper.GetKbn<EnumSample>(EnumSample.Kbn2));
        }
        [Fact]
        public void Test3()
        {
            Assert.Equal("山本山", this.helper.GetKbn<EnumSample>(EnumSample.KbnYamamoto));
        }
        [Fact]
        public void Test4()
        {
            Assert.Equal("1:test", this.helper.GetKbn<EnumSample>(EnumSample.Kbn1, 2));
        }
        [Fact]
        public void Test5()
        {
            Assert.Equal("2:aaaa", this.helper.GetKbn<EnumSample>(EnumSample.Kbn2, 2));
        }
        [Fact]
        public void Test6()
        {
            Assert.Equal("3:山本山", this.helper.GetKbn<EnumSample>(EnumSample.KbnYamamoto, 2));
        }
        [Fact]
        public void Test7()
        {
            var list = this.helper.GetAll<EnumSample>();
            Assert.Equal<int>(3, list.Count());
            Assert.Equal("test", list.ElementAt(0).Name);
            Assert.Equal("aaaa", list.ElementAt(1).Name);
            Assert.Equal("山本山", list.ElementAt(2).Name);
            Assert.Equal("1:test", list.ElementAt(0).IdAndName);
            Assert.Equal("2:aaaa", list.ElementAt(1).IdAndName);
            Assert.Equal("3:山本山", list.ElementAt(2).IdAndName);
        }
    }

    public class EnumSample
    {
        [Enum(Name = "test", IdAndName = "1:test")]
        public readonly static int Kbn1 = 1;
        [Enum(Name = "aaaa", IdAndName = "2:aaaa")]
        public readonly static string Kbn2 = "aaaa";
        [Enum(Name = "山本山", IdAndName = "3:山本山")]
        public readonly static string KbnYamamoto = "ccc";
    }
}
