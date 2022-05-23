using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Coorth.Framework;

internal class IndexDictTest {

    [Test]
    public void Add_KeyValueX1() {
        var dict = new IndexDict<int>(4);
        dict.Add(1, 2);
        Assert.IsTrue(dict.ContainsKey(1));
        Assert.IsTrue(dict.Count == 1);
    }

    [Test]
    public void Add_KeyValueX2() {
        var dict = new IndexDict<int>(4);
        dict.Add(1, 2);
        dict.Add(3, 4);
            
        Assert.IsTrue(dict.ContainsKey(1));
        Assert.IsTrue(dict.ContainsKey(3));
        Assert.IsTrue(dict.Count == 2);
    }

    [Test]
    public void Add_KeyValueX100() {
        var dict = new IndexDict<int>(4);
        for(var i=0; i< 100; i++) {
            dict.Add((ushort)i, i);
        }
        Assert.IsTrue(dict.Count == 100);
    }
        
    [Test]
    public void Add_KeyHashConflict() {
        var dict = new IndexDict<int>(4);
        dict.Add(1, 2);
        dict.Add(5, 4);

        Assert.IsTrue(dict.ContainsKey(1));
        Assert.IsTrue(dict.ContainsKey(5));
        Assert.IsFalse(dict.ContainsKey(3));
        Assert.IsTrue(dict.Count == 2);
    }

    [Test]
    public void Add_MultiKeyHashConflict() {
        var dict = new IndexDict<int>(4);
        dict.Add(1, 2);
        dict.Add(5, 4);
        dict.Add(9, 5);
        dict.Add(6, 6);

        Assert.IsTrue(dict.ContainsKey(1));
        Assert.IsTrue(dict.ContainsKey(5));
        Assert.IsTrue(dict.ContainsKey(9));
        Assert.IsTrue(dict.ContainsKey(6));

        Assert.IsTrue(dict.Get(1) == 2);
        Assert.IsTrue(dict.Get(5) == 4);
        Assert.IsTrue(dict.Get(9) == 5);
        Assert.IsTrue(dict.Get(6) == 6);
    }

    [Test]
    public void Add_KeyDuplicate() {
        var dict = new IndexDict<int>(4);
        dict.Add(1, 2);
            
        Assert.Catch<ArgumentException>(() => dict.Add(1, 4));
    }

    [Test]
    public void ContainsKey() {
        var dict = new IndexDict<int>(4);
        dict.Add(1, 2); 
        dict.Add(3, 2);
        Assert.IsTrue(dict.ContainsKey(1));
        Assert.IsTrue(dict.ContainsKey(3));
        Assert.IsFalse(dict.ContainsKey(4));
    }

    [Test]
    public void Get() {
        var dict = new IndexDict<int>(4);
        dict.Add(1, 2);
        dict.Add(3, 6);
        Assert.IsTrue(dict.Get(1) == 2);
        Assert.IsTrue(dict.Get(3) == 6);

        Assert.Catch<KeyNotFoundException>(() => dict.Get(7));
    }

    [Test]
    public void SpecialList() {
        var dict = new IndexDict<int>(2);
        dict.Add(10, 1);
        dict.Add(19, 1);
        dict.Add(11, 1);
        dict.Add(18, 1);
        dict.Add(20, 1);

        Assert.IsTrue(dict.Count == 5);
            
        Assert.IsTrue(dict.Get(10) == 1);
        Assert.IsTrue(dict.Get(19) == 1);
        Assert.IsTrue(dict.Get(11) == 1);
        Assert.IsTrue(dict.Get(18) == 1);
        Assert.IsTrue(dict.Get(20) == 1);

        //10 -> 19 -> 11 -> 18 -> 20
    }
        
    [Test]
    public void SpecialList2() {
        var dict = new IndexDict<int>(2);
        dict[10] = 1;
        dict[19] = 1;
        dict[11] = 1;
        dict[18] = 1;
        dict[20] = 1;

        Assert.IsTrue(dict.Count == 5);
            
        Assert.IsTrue(dict.Get(10) == 1);
        Assert.IsTrue(dict.Get(19) == 1);
        Assert.IsTrue(dict.Get(11) == 1);
        Assert.IsTrue(dict.Get(18) == 1);
        Assert.IsTrue(dict.Get(20) == 1);

        //10 -> 19 -> 11 -> 18 -> 20
    }

    [Test]
    public void Resize() {
        var dict = new IndexDict<int>(4);
        for (var i = 0; i < 100; i++) {
            dict.Add((ushort)i, i * 2);
        }
        for (var i = 0; i < 100; i++) {
            Assert.IsTrue(dict.ContainsKey((ushort)i));
            Assert.IsTrue(dict[(ushort)i] == i*2);
        }
    }

    [Test]
    public void Set() {
        var dict = new IndexDict<int>(4);
        dict.Add(1, 2);
        dict[3] = 6;
        Assert.IsTrue(dict.Get(1) == 2);
        Assert.IsTrue(dict.Get(3) == 6);
    }

    [Test]
    public void Remove() {
        var dict = new IndexDict<int>(4);
        dict.Add(1, 2);
        dict.Add(3, 6);
        Assert.IsTrue(dict.Count == 2);


        var result = dict.Remove(1);
        Assert.IsTrue(result);
        Assert.IsTrue(dict.Count == 1);
        Assert.IsFalse(dict.ContainsKey(1));
        Assert.IsTrue(dict.ContainsKey(3));

        Assert.IsFalse(dict.Remove(1));
    }

    [Test]
    public void Clear() {
        var dict = new IndexDict<int>(4);
        dict.Add(1, 2);
        dict.Add(3, 6);
        dict.Clear();

        Assert.IsTrue(dict.Count == 0);
        Assert.IsFalse(dict.ContainsKey(1));
        Assert.IsFalse(dict.ContainsKey(3));
    }
}