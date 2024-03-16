.. title:: Home

.. ifconfig:: RUNNING_IN_DOCKER

   .. include:: ../../README.md
      :parser: myst_parser.sphinx_

.. ifconfig:: not RUNNING_IN_DOCKER

   .. include:: ../../../README.md
      :parser: myst_parser.sphinx_

.. toctree::
   :hidden:
   :maxdepth: -1

   Home <self>


.. toctree::
   :hidden:
   :maxdepth: -1
   :caption: The Basics

   overview/get_started.rst


.. toctree::
   :hidden:
   :maxdepth: -1
   :caption: Background

   overview/rationale.rst


.. toctree::
   :hidden:
   :maxdepth: -1
   :caption: Demos

   demos/basics.rst
   demos/examples.rst

.. toctree::
   :hidden:
   :maxdepth: -1
   :caption: API Reference

   api_reference


.. Planned headers
.. Contributing (contributing guidelines)
.. Deep Dive (dev-related stuff)