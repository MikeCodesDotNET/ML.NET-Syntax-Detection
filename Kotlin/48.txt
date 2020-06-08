val jake = Person()                                     // 1
val stringDescription = jake.apply {                    // 2
    name = "Jake"                                       // 3
    age = 30
    about = "Android developer"
}.toString()                                            // 4