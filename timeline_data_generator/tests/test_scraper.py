from timeline_scraper.main import main
import pytest
import os

def test_main(tmpdir):
    # main(f"{tmpdir}/data.csv")
    # assert os.path.exists(f"{tmpdir}/test.csv")
    main(f"../data.csv")