import pandas as pd

from .scrapers import ScrapeDiscoveries


def main(csv_path:str):
    scrapers = [
        ScrapeDiscoveries
    ]

    all_data = []
    for scraper in scrapers:
        data:pd.DataFrame = scraper().scrape()
        all_data.append(data)

    all_data = pd.concat(all_data)
    all_data.to_csv(csv_path, index=False)
    print(f"Saved to {csv_path}")
