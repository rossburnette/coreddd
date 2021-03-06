﻿using System.Collections.Generic;
using CoreUtils.Extensions;
using NUnit.Framework;
using Shouldly;

namespace CoreUtils.Tests.Extensions.CollectionExtensions
{
    [TestFixture]
    public class when_invoking_action_for_each_item_with_index
    {
        private List<int> _list;
        private int[] _items;
        private List<int> _indexList;

        [SetUp]
        public void Context()
        {
            _items = new[] {1, 2, 3};
            _indexList = new List<int>();
            _list = new List<int>();

            _items.Each((i, x) =>
            {
                _indexList.Add(i);
                _list.Add(x);
            });
        }

        [Test]
        public void all_items_are_added_to_list()
        {
            _list.ShouldBe(_items);
        }

        [Test]
        public void indexes_generated_correctly()
        {
            _indexList.ShouldBe(new[] {0, 1, 2});
        }

    }
}