val jake = Person("Jake", 30, "Android developer")   // 1
    .also {                                          // 2 
        writeCreationLog(it)                         // 3
    }