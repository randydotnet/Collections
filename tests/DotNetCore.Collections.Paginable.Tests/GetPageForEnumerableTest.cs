﻿using System.Collections.Generic;
using System.Linq;
using DotNetCore.Collections.Paginable.Tests.Models;
using Xunit;

namespace DotNetCore.Collections.Paginable.Tests {
    public class GetPageForEnumerableTest {

        private IList<Student> EmptyListForStudents { get; set; }
        private IList<Student> OneItemForStudents { get; set; }
        private IList<Student> EightItemsForStudents { get; set; }

        public GetPageForEnumerableTest() {
            EmptyListForStudents = new List<Student>();
            OneItemForStudents = new List<Student> {new Student {Id = 1, Name = "Alex"}};
            EightItemsForStudents = new List<Student> {
                new Student {Id = 2, Name = "Zhaomin"},
                new Student {Id = 3, Name = "Zhaomin"},
                new Student {Id = 4, Name = "Zhaomin"},
                new Student {Id = 5, Name = "Zhaomin"},
                new Student {Id = 6, Name = "Zhaomin"},
                new Student {Id = 7, Name = "Zhaomin"},
                new Student {Id = 8, Name = "Zhaomin"},
                new Student {Id = 9, Name = "Zhaomin"},
            };
        }

        [Fact]
        public void EmptyListTest() {
            var page = EmptyListForStudents.GetPage(1, 9);
            Assert.Equal(1, page.TotalPageCount);
            Assert.Equal(0, page.TotalMemberCount);
            Assert.Equal(1, page.CurrentPageNumber);
            Assert.Equal(9, page.PageSize);
            Assert.Equal(0, page.CurrentPageSize); //应该是0
            Assert.False(page.HasNext);
            Assert.False(page.HasPrevious);
        }

        [Fact]
        public void EmptyListToOriginTest() {
            var page = EmptyListForStudents.GetPage(1, 9);
            var origins = page.ToOrigonItems();
            Assert.Equal(0, origins.Count());

            var counter = 0;
            foreach (var item in origins) counter++;
            Assert.Equal(0, counter);
        }

        [Fact]
        public void OneItemTest() {
            var page = OneItemForStudents.GetPage(1, 9);
            Assert.Equal(1, page.TotalPageCount);
            Assert.Equal(1, page.TotalMemberCount);
            Assert.Equal(1, page.CurrentPageNumber);
            Assert.Equal(9, page.PageSize);
            Assert.Equal(1, page.CurrentPageSize); //应该是0
            Assert.False(page.HasNext);
            Assert.False(page.HasPrevious);
        }

        [Fact]
        public void OneItemToOriginTest() {
            var page = OneItemForStudents.GetPage(1, 9);
            var origins = page.ToOrigonItems();
            Assert.Equal(1, origins.Count());

            var counter = 0;
            foreach (var item in origins) counter++;
            Assert.Equal(1, counter);
        }

        [Fact]
        public void OnePageTest() {
            var page = EightItemsForStudents.GetPage(1, 9);
            Assert.Equal(1, page.TotalPageCount);
            Assert.Equal(8, page.TotalMemberCount);
            Assert.Equal(1, page.CurrentPageNumber);
            Assert.Equal(9, page.PageSize);
            Assert.Equal(8, page.CurrentPageSize);
            Assert.False(page.HasNext);
            Assert.False(page.HasPrevious);
        }
        
        [Fact]
        public void OnePageoOriginTest() {
            var page = EightItemsForStudents.GetPage(1, 9);
            var origins = page.ToOrigonItems();
            Assert.Equal(8, origins.Count());

            var counter = 0;
            foreach (var item in origins) counter++;
            Assert.Equal(8, counter);
        }

        [Fact]
        public void SeveralPagesTest() {
            var page = EightItemsForStudents.GetPage(1, 2);
            Assert.Equal(4, page.TotalPageCount);
            Assert.Equal(8, page.TotalMemberCount);
            Assert.Equal(1, page.CurrentPageNumber);
            Assert.Equal(2, page.PageSize);
            Assert.Equal(2, page.CurrentPageSize);
            Assert.True(page.HasNext);
            Assert.False(page.HasPrevious);
        }
        
        [Fact]
        public void SeveralPagesOriginTest() {
            var page = EightItemsForStudents.GetPage(1, 2);
            var origins = page.ToOrigonItems();
            Assert.Equal(2, origins.Count());

            var counter = 0;
            foreach (var item in origins) counter++;
            Assert.Equal(2, counter);
        }
    }
}