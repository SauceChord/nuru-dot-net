<img width="100%" src="nuru-logo.png" alt="nuru dot net"> 

This project is going through it's fourth re-iteration on the design. After learning about pitfalls as I went along I have been rethinking my approach and as a result have been throwing the baby out with the bathwater. This time around I will also be thinking more about the design up front. 

# Design diagrams

## Image

So as I am starting over, I toyed a bit with the idea of the image. Previously I had no way to differentiate between ANSI4, ANSI8 and TrueColor for example. Also the previous implementation had no good way of supporting keys. This time around I am placing the burden of accessing the correct color methods on the library user. Any ideas on how to improve on this is greatly appreciated, though. 

![Class diagram puml/image1.puml](http://www.plantuml.com/plantuml/png/XL71JeGm4BttAoPxOYFh1t0mgoOIOjIDnfFnCC80pPQMJ8kHRFntYq00WtffPjvxq_SwtLk7x9fAYLIXjQ0RRiwJ2Wk2am0u-ou3sPqtKb48DIfoZWvitcL6lEr4CRdh4Zdu2A4TWSlxfsG1FCPNY_wE74v0Oc-f7TfFiGZpuFCCzZ-9Gn-cfwHsn3ccDCtD-34md0R_MBLrsJ61negs4C5CFRxAACE_QdpEFvrsBndQahBNtSLgucdNQzAU-L-uRkwZ6KV0iYWTc7pshr6qcIrx8m2UBUMDWjmmsAEiQwaBODB-bgxoWhH4njGHWz5mfDO38UG5-86tGdWbT6PYxwkcKby0)

Further looking at IO, adding a simple layer that reads and writes data in nurus format with respect to big endian and string lengths of 7 looks like this:

![Class diagram puml/format1.puml](http://www.plantuml.com/plantuml/png/bPD1I_j04CNl-HHp_9S_BQWWua6gOY4dnI8UnCCsEyakR7RBpaHQnEzkDeans17gRURtEfdtThTBOXMvgLrME4K4ledD8hzVvBMgS6KT_k0FoY6pfAn2h1K_ej8OuJq3c6-ixzks-P-gw_Ljdl-RmIKKHetqoTiYQ3pWQuxMLnschXm7SiyN7T_i6QVqvBp33PaGUIgSdVQ2zPnzYCQVe-M9yTJsnhkgT_wcN8E3ozceVVcNrHwF6GGXXH1m7q8GKeXPcsA8iTuJOvs2t5YluduuSbaSOcK7rmpp5wt8e7wTNubN-gUHOOtmQ8xPS_pGk4dwqdJgjrCIFYRCIGSO6C5XoH1AEGV0j0NbDIGsNOIxRI1CM07PUkiGQcGJD11omhJ5mfQsW3B4RWkKrY4jClGQaeN1IBIL4PoCZ_ZEQwkyHwBaemMbtQ7-_bivz1KRK17RnhG_SQrgCE9aqxAjcjfz0W00)

Which is used by ImageFile and PaletteFile:

![Class diagram puml/imagefile1.puml](http://www.plantuml.com/plantuml/png/XPFHRzCm4CRV_LTOVMgtTLe6Ge24D8ZK4Q4Mj13sR9AZiN3ioJuZ8jJ_dLpcUJK9wbE-xxlBxsmdld4eBFfECT2-uzfRVrbylhnL_KExDGtmFuppdT70t_6hGRulyw8OT3ReIilQD5Iv7jmNeG0HDb81rQ_Vi4FAp8qoTfQvAukNWKdwzJpnnTM_n2sWc0DMXKQ2P45dhqRYWTLAE1TVBpgn7-avA3KoplDMMFwMryE3xCPO6Ept7e7i1r7_FB8qVh3UjSRY_-cFvDfeXxoKUotGMrhfObVTLSNsTl5S_WRMIQF3qi_ZLo3pT7nnz9E-bmss93w2tBT8wXFqj9CWQHT1qWuYEPnlX3_xTGrAEJPs0jBnB7zVy7uL0c8d0Kr80fgJ1BJW451I7b4l-2-XFAn27k5f7cUaUHmroUF4c5FXgodHWkYMFupj1DvRYM1NISMDZv3H1rm-26mVNtSeKDPyOqsNSEv0D84pQOd0j7X4FZ0slNZhzHbVP8lf_yJI2tMwDrwLqwtJQp7sdytrtu1kVAV-0W00)


![Class diagram puml/palettefile1.puml](http://www.plantuml.com/plantuml/png/RLBFJy8m5BxlhsXssiIHe1TZD05C84I3P6Dw81oUwnCQjuwqhqPY-DzTeU2QxBJlHz-NRwyTQW95fYe3bAPYqYZJdxts5r0Y4IxtEsI_0MEZUJQxOuziqE1SYgBcsB2RXgNJSODkWqDGbA3r9S7gThAN8cVtx6DFQDbK8SeM7vS6pzJoPPsH4d8JnLvYgzD4b7XABcgfYMLY8u6CieN2UPxcYz5B-6-_eTAYbgvxh_LZbZxZ_caBoi59hSxg68el2tEDYWD1WnEKf0HgLtUrFXQU8XsN4md9yITsaWtrbMOVPy4gcJTspRwXD1Wxnqvt6ZR4royxGwYYprfLGEzA4AhOS_JnupIxmMW7j3sDQm8I1Pkekl9oKWJkShmZNg1ljf8FGT3rc9BaYeM3iEjIW_RJUKXM7FGM-JhfF3j4oKrL_W40)