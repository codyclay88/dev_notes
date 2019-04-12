An Image is essentially a definition of binaries and dependencies, as well as metadata about the image and how to run it. 

An image is made up of layers. Each of these layers are stacked on top of each other to create the final image. Each layer is considered "immutable", meaning that once the layer is created, it cannot be changed or modified in any way. This means that we can reliably share layers between multiple images without having to worry about one layer changing underneath us. 

Because of this, these "layers" are stored in an image cache. This means that we can store the layers that we use so that if another image also relies on that layer then we dont need to redownload it, because we already have it. 